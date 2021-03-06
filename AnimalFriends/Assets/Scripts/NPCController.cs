﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCController : MonoBehaviour
{
    public float speed, jumpPower, defaultColliderSize, defaultColliderOffsetY;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D collider;
    public bool canMove;
    bool isPiggyback = false;
    bool isPlayJumpVoice = true;

    enum State { Init = -1, Idle = 0, Walk = 1, JumpUp = 2, JumpDown = 3, KnockBack = 4, Squat = 5, };
    enum Direction { Right = 0, Left = 1, };

    State state;
    Direction direction = Direction.Right;
    float coolTime = 0;
    float idlingTime = 0;
    float squatIdlingTime = 0;

    int jumpCount;

    private Dictionary<string, bool> movementType = new Dictionary<string, bool>();
    private Dictionary<string, bool> nextMovementType = new Dictionary<string, bool>();

    float motionTime = 0;
    float nextMotionTime = 0;
    int movementIndex = 0;

    TextController textController;
    GameController gameController;

    private void Awake()
    {
        textController = GameObject.Find("windowTextRight").GetComponent<TextController>();
        gameController = GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
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


        Vector2 newVelocity = Move();
        state = CalcState(newVelocity.x, newVelocity.y);
        animator.SetInteger("state", (int)state);

        Vector2 colliderSize = new Vector2(defaultColliderSize, defaultColliderSize);
        Vector2 colliderOffset;
        if (isPiggyback)
        {
            colliderOffset = new Vector2(0, -77);
        }
        else
        {
            colliderOffset = new Vector2(0, defaultColliderOffsetY);
        }


        if (state == State.Squat)
        {
            colliderSize.y = defaultColliderSize / 2;
            if (isPiggyback)
            {
                colliderOffset.y = -77 - (defaultColliderSize / 4);
            }
            else
            {
                colliderOffset.y = defaultColliderOffsetY - (defaultColliderSize / 4);
            }
        }
        collider.size = colliderSize;
        collider.offset = colliderOffset;

        coolTime -= Time.deltaTime;

    }

    Vector2 Move()
    {
        Vector2 scale = transform.localScale;
        Vector2 newVelocity;
        float velocityX = 0;
        float velocityY = rb.velocity.y;

        if (StaticController.stageNumber == 3 && canMove)
        {

            if (Input.GetKey("left"))
            {
                idlingTime = 0;
                direction = Direction.Left;
                velocityX = -speed;
                scale.x = Math.Abs(scale.x) * -1;
            }
            else if (Input.GetKey("right"))
            {
                idlingTime = 0;
                direction = Direction.Right;
                velocityX = speed;
                scale.x = Math.Abs(scale.x);
            }
            else
            {
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
                velocityY = jumpPower;
                jumpCount++;
            }
        }
        else
        {

            if (movementType.Count == 0 || motionTime < 0.1f)
            {
                // 何も入力が無い場合
                if (nextMovementType.Count == 0)
                {
                    newVelocity = new Vector2(velocityX, velocityY);
                    rb.velocity = newVelocity;
                    return newVelocity;
                }
                if (isPlayJumpVoice)
                {
                    textController.UpdateNewText("ゴー！", TextController.EyeType.Cross, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("maki-go", 1.0f);
                }
                movementType = new Dictionary<string, bool>(nextMovementType);
                nextMovementType.Clear();
                motionTime = nextMotionTime;
            }
            else
            {
                motionTime -= Time.deltaTime;
            }

            if (movementType["isRight"])
            {
                direction = Direction.Right;
                velocityX = speed;
                scale.x = Math.Abs(scale.x);
            }
            else if (movementType["isLeft"])
            {
                direction = Direction.Left;
                velocityX = -speed;
                scale.x = Math.Abs(scale.x) * -1;
            }


            if (movementType["isSquat"])
            {
                squatIdlingTime += Time.deltaTime;
            }
            else
            {
                squatIdlingTime = 0;
            }


            if (movementType["isJump"] && !movementType["isJumped"] && jumpCount < 2)
            {
                if (isPlayJumpVoice)
                {
                    int jumpRand = (int)UnityEngine.Random.Range(0.0f, 7.0f);
                    switch (jumpRand)
                    {
                        case 0:
                            textController.UpdateNewText("てい！", TextController.EyeType.Smile, TextController.Priority.Low);
                            AudioManager.Instance.PlaySE("maki-jump-0", 1.0f);
                            break;
                        case 1:
                            textController.UpdateNewText("やあ！", TextController.EyeType.Anger, TextController.Priority.Low);
                            AudioManager.Instance.PlaySE("maki-jump-1", 1.0f);
                            break;
                        case 2:
                            textController.UpdateNewText("ジャンプ！", TextController.EyeType.Wink, TextController.Priority.Low);
                            AudioManager.Instance.PlaySE("maki-jump-2", 1.0f);
                            break;
                        case 3:
                            textController.UpdateNewText("うりゃ！", TextController.EyeType.Cross, TextController.Priority.Low);
                            AudioManager.Instance.PlaySE("maki-jump-3", 1.0f);
                            break;
                        case 4:
                            textController.UpdateNewText("ほいっ", TextController.EyeType.Normal, TextController.Priority.Low);
                            AudioManager.Instance.PlaySE("maki-jump-4", 1.0f);
                            break;
                        case 5:
                            textController.UpdateNewText("たあ！", TextController.EyeType.Smile, TextController.Priority.Low);
                            AudioManager.Instance.PlaySE("maki-jump-5", 1.0f);
                            break;
                        case 6:
                            textController.UpdateNewText("えい！", TextController.EyeType.Cross, TextController.Priority.Low);
                            AudioManager.Instance.PlaySE("maki-jump-6", 1.0f);
                            break;
                    }
                }
                movementType["isJumped"] = true;
                velocityY = jumpPower;
                jumpCount++;
            }


            transform.localScale = scale;
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

            case "CommandObject":
                CommandObjectController controller = collision.gameObject.GetComponent<CommandObjectController>();

                addMoveDict(controller.isForceType, controller.isJump, controller.isRight, controller.isLeft, controller.isSquat, controller.motionTime);
                break;

            case "TalkObject":
                TalkObjectController talkController = collision.gameObject.GetComponent<TalkObjectController>();
                if (talkController.isMaki)
                {
                    if (talkController.isSpeak)
                    {
                        AudioManager.Instance.PlaySE(talkController.voiceFileName, talkController.seVolume);
                    }
                    textController.UpdateNewText(talkController.text, talkController.eyeType, talkController.priority, talkController.addTextInterval);
                    Destroy(collision.gameObject);
                }
                break;

            case "Goal":
                gameController.NpcGoal();
                break;

            default:
                break;
        }
    }

    public void addMoveDict(bool isForceType, bool isJump, bool isRight, bool isLeft, bool isSquat, float motionTime)
    {
        var dict = new Dictionary<string, bool>();
        dict.Add("isForceType", isForceType);
        dict.Add("isJump", isJump);
        dict.Add("isJumped", false);
        dict.Add("isRight", isRight);
        dict.Add("isLeft", isLeft);
        dict.Add("isSquat", isSquat);
        if (isForceType)
        {
            this.motionTime = motionTime;
            this.movementType = dict;
        }
        else if (nextMovementType.Count == 0)
        {
            this.nextMotionTime = motionTime;
            this.nextMovementType = dict;
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void SetIsPiggyback(bool isPiggyback)
    {
        this.isPiggyback = isPiggyback;
    }

    public void SetIsPlayJumpVoice(bool isPlayJumpVoice)
    {
        this.isPlayJumpVoice = isPlayJumpVoice;
    }
}
