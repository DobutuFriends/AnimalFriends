using UnityEngine;
using UniRx;
using System;

public class PlayerControl : MonoBehaviour
{
    public float SPEED;
    public float JUMP_POWER;
    private int isRunningId = Animator.StringToHash("isRunning");
    private Rigidbody2D rb;

    [SerializeField]
    BulletController bullet;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Observable.EveryUpdate()
                  .Where(_ => Input.GetKey("z"))
                  .Sample(TimeSpan.FromSeconds(1.0f))
                  .Subscribe(x => InstantiateBullet())
                  .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

        if (Input.GetKey("up"))
        {
            rb.velocity = new Vector2(0, JUMP_POWER);
        }

        transform.position = position;
        transform.localScale = scale;
    }
}
