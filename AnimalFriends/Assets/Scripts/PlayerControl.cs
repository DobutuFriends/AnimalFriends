using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float SPEED;
    public float JUMP_POWER;
    private int isRunningId = Animator.StringToHash("isRunning");
    private int key;
    private Rigidbody2D rb;
    private Animator animator;
    private float walkForce = 60.0f;//歩き力
    private float maxWalkSpeed = 10.0f;//速度制限

    [SerializeField]
    BulletController bullet;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Observable.EveryUpdate()
                  .Where(_ => Input.GetKey("z"))
                  .Sample(TimeSpan.FromSeconds(1.0f))
                  .Subscribe(x => InstantiateBullet())
                  .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAddForce();
        Retry();
    }

    private void InstantiateBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    void Move()
    {
        Vector2 position = transform.position;
        Vector2 scale = transform.localScale;
        if (Input.GetKey("left"))
        {
            position.x -= SPEED;
            scale.x = -1;
        }
        else if (Input.GetKey("right"))
        {
            position.x += SPEED;
            scale.x = 1;
        }

        if (Input.GetKey("up") && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(0, JUMP_POWER);
        }

        transform.position = position;
        transform.localScale = scale;

        // プレイヤの速度に応じてアニメーション速度を変える
        //地面＝歩行アニメ＝ジャンプ中ではない
        if (rb.velocity.y == 0)
        {
            this.animator.speed = SPEED / 2.0f;

        }
    }


    void MoveAddForce()
    {
        // 左右移動
        key = 0;//何も押されていないときは0とすること＝歩き力0
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // プレイヤの速度
        SPEED = Mathf.Abs(rb.velocity.x);

        // スピード制限：MAXスピード未満なら加速（減速）させる
        if (SPEED < this.maxWalkSpeed)
        {
            rb.AddForce(transform.right * key * this.walkForce);
        }


        // 反転対策：歩く向きによって、体を反転させる
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }


        // ジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.AddForce(transform.up * JUMP_POWER);
            //UIテスト：HP
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();
        }


        // プレイヤの速度に応じてアニメーション速度を変える
        //地面＝歩行アニメ＝ジャンプ中ではない
        if (rb.velocity.y == 0)
        {
            this.animator.speed = SPEED / 2.0f;

        }
        //ジャンプ中のアニメーション速度を一定とする
        else
        {
            this.animator.speed = 1.5f;
        }

    }




    void Retry()
    {
            if (transform.position.y < -10)
        {
            SceneManager.LoadScene("MainScene");
        }
    }



    // ゴールに到達
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ゴール");
        //SceneManager.LoadScene("ClearScene");
    }
}
