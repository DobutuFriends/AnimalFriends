using UnityEngine;
using UniRx;
using System;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpPower;
    public float attackInterval;
    public float dashReceptionTime;
    public float defaultColliderSize;
    public float defaultColliderOffsetY;
    public bool canMove;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D collider;
    enum State { Init = -1, Idle = 0, Walk = 1, JumpUp = 2, JumpDown = 3, KnockBack = 4, Squat = 5, };
    enum Direction { Right = 0, Left = 1, };

    State state;
    Direction direction = Direction.Right;
    float coolTime = 0;
    float idlingTime = 0;
    float squatIdlingTime = 0;
    bool isDash = false;

    float defaultGaugeWidth;
    int jumpCount;

    [SerializeField]
    PhysicalAttackController physicalAttack1, physicalAttack2, physicalAttack3;

    TextController textController;

    private void Awake()
    {
        textController = GameObject.Find("windowTextLeft").GetComponent<TextController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        state = State.Init;
    }

    private void Init()
    {
        state = State.Idle;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Init)
        {
            Init();
        }

        if (!canMove)
        {
            rb.velocity = new Vector2(0, 0);
            state = State.Idle;
        }
        else
        {

            Vector2 newVelocity = Move();
            state = CalcState(newVelocity.x, newVelocity.y);
        }

        animator.SetInteger("state", (int)state);

        Vector2 colliderSize = new Vector2(defaultColliderSize, defaultColliderSize);
        Vector2 colliderOffset = new Vector2(0, defaultColliderOffsetY);
        if (state == State.Squat)
        {
            colliderSize.y = defaultColliderSize / 2;
            colliderOffset.y = defaultColliderOffsetY * 1.5f;
        }
        collider.size = colliderSize;
        collider.offset = colliderOffset;

        coolTime -= Time.deltaTime;
    }

    Vector2 Move()
    {
        SummarSolt();
        Vector2 scale = transform.localScale;
        Vector2 newVelocity;
        float velocityX = rb.velocity.x;
        float velocityY = rb.velocity.y;

        if (coolTime > 0 && state == State.KnockBack)
        {
            return rb.velocity;
        }

        if (idlingTime < dashReceptionTime &&
            ((Input.GetKeyDown("left") && direction == Direction.Left) || (Input.GetKeyDown("right") && direction == Direction.Right)))
        {
            isDash = true;
        }

        if (Input.GetKey("left"))
        {
            idlingTime = 0;
            direction = Direction.Left;
            velocityX = -speed;
            if (isDash)
            {
                velocityX *= 2;
            }
            scale.x = Math.Abs(scale.x) * -1;
        }
        else if (Input.GetKey("right"))
        {
            idlingTime = 0;
            direction = Direction.Right;
            velocityX = speed;
            if (isDash)
            {
                velocityX *= 2;
            }
            scale.x = Math.Abs(scale.x);
        }
        else
        {
            isDash = false;
            idlingTime += Time.deltaTime;

            if (Input.GetKey("down"))
            {
                squatIdlingTime += Time.deltaTime;
            }
            else
            {
                squatIdlingTime = 0;
            }
            velocityX = 0;
        }

        transform.localScale = scale;

        if (Input.GetKeyDown("c") && jumpCount < 2)
        {
            if (jumpCount == 0)
            {
                textController.UpdateNewText("えい！", TextController.EyeType.Cross, TextController.Priority.Low);
                AudioManager.Instance.PlaySE("jump01", 0.2f);
            }
            else
            {
                textController.UpdateNewText("とお！", TextController.EyeType.Anger, TextController.Priority.Low);
                AudioManager.Instance.PlaySE("jump02", 0.2f);
            }

            velocityY = jumpPower;
            jumpCount++;
        }

        // Debug
        if (Input.GetKeyDown("1"))
        {
            textController.UpdateNewText("結月ゆかりです。", TextController.EyeType.Normal, TextController.Priority.Low);
        }
        else if (Input.GetKeyDown("2"))
        {
            textController.UpdateNewText("うふふ", TextController.EyeType.Smile, TextController.Priority.Low);
        }
        else if (Input.GetKeyDown("3"))
        {
            textController.UpdateNewText("闇の炎に抱かれて消えろっ！", TextController.EyeType.Anger, TextController.Priority.Low);
        }
        else if (Input.GetKeyDown("4"))
        {
            textController.UpdateNewText("結月ゆかりです♪", TextController.EyeType.Wink, TextController.Priority.Low);
        }
        else if (Input.GetKeyDown("5"))
        {
            textController.UpdateNewText("バカなっ", TextController.EyeType.Cross, TextController.Priority.Low);
        }

        newVelocity = new Vector2(velocityX, velocityY);
        rb.velocity = newVelocity;
        return newVelocity;
    }

    State CalcState(float velocityX, float velocityY)
    {
        if (state == State.KnockBack && coolTime >= 0)
        {
            return State.KnockBack;
        }
        if (velocityY > 0.0f)
        {
            return State.JumpUp;
        }
        else if (velocityY < 0.0f)
        {
            return State.JumpDown;
        }
        else if (System.Math.Abs(velocityX) < 1)
        {
            if (squatIdlingTime > 0)
            {
                return State.Squat;
            }
            return State.Idle;
        }
        else
        {
            return State.Walk;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                coolTime = 0.5f;
                if (transform.localPosition.x < collision.transform.localPosition.x)
                {
                    rb.velocity = new Vector2(-500.0f, 1000.0f);
                }
                else
                {
                    rb.velocity = new Vector2(500.0f, 1000.0f);
                }
                state = State.KnockBack;
                break;
            case "MapObject":
                //jumpCount = 0;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "MapObject":
                jumpCount = 0;
                break;

            case "TalkObject":
                TalkObjectController talkController = collision.gameObject.GetComponent<TalkObjectController>();
                if (talkController.isYukari)
                {
                    if (talkController.isSpeak)
                    {
                        AudioManager.Instance.PlaySE(talkController.voiceFileName, talkController.seVolume);
                    }
                    textController.UpdateNewText(talkController.text, talkController.eyeType, talkController.priority, talkController.addTextInterval);
                    Destroy(collision.gameObject);
                }
                break;

            default:
                break;
        }
    }

    private void SummarSoltStart()
    {
        transform.Rotate(new Vector3(0, 0, 24));
        float velocityX = rb.velocity.x; ;
        float velocityY = jumpPower / 2;
        rb.velocity = new Vector2(velocityX, velocityY);
    }

    private void SummarSolt()
    {
        if (transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.Rotate(new Vector3(0, 0, 24));
        }
    }
}
