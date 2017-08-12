﻿using UnityEngine;
using UniRx;
using System;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpPower;
    public float attackInterval;
    private Rigidbody2D rb;
    private Animator animator;
    enum State { Idle = 0, Walk = 1, JumpUp = 2, JumpDown = 3, KnockBack = 4, };
    enum AttackState { Idle = 0, Attack1 = 1, Attack2 = 2, Attack3 = 3, SummerSalt = 4, }
    enum Direction { Right = 0, Left = 1, };

    State state;
    AttackState attackState;
    Direction direction = Direction.Right;
    float coolTime = 0;

    public int maxHp;
    int hp;
    GameObject hpGauge;
    float defaultGaugeWidth;

    [SerializeField]
    PhysicalAttackController physicalAttack1, physicalAttack2, physicalAttack3;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hpGauge = gameObject.transform.Find("hpGauge").gameObject;
        defaultGaugeWidth = hpGauge.transform.localScale.x;

        state = State.Idle;
        attackState = AttackState.Idle;
        hp = maxHp;

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown("x"))
            .Sample(TimeSpan.FromSeconds(0.1f))
            .Subscribe(x => Attack())
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newVelocity = Move();
        state = CalcState(newVelocity.x, newVelocity.y);
        animator.SetInteger("state", (int)state);
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

        if (Input.GetKey("left"))
        {
            direction = Direction.Left;
            velocityX = -speed;
            scale.x = Math.Abs(scale.x) * -1;
        }
        else if (Input.GetKey("right"))
        {
            direction = Direction.Right;
            velocityX = speed;
            scale.x = Math.Abs(scale.x);
        }
        else
        {
            velocityX = 0;
        }

        transform.localScale = scale;

        if (Input.GetKeyDown("c"))
        {
            velocityY = jumpPower;
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
        if (velocityY > 0)
        {
            return State.JumpUp;
        }
        else if (velocityY < 0)
        {
            return State.JumpDown;
        }
        else if (System.Math.Abs(velocityX) < 1)
        {
            return State.Idle;
        }
        else
        {
            return State.Walk;
        }
    }

    private void Attack()
    {
        if (coolTime > 0)
        {
            return;
        }
        Vector2 attackPosition = transform.position;
        Quaternion attackRotation = transform.rotation;

        if (Input.GetKey("up"))
        {
            SummarSoltStart();
        }

        if (direction == Direction.Right)
        {
            attackPosition.x += 80;
            attackPosition.y -= 20;
        }
        else
        {
            attackPosition.x -= 80;
            attackPosition.y -= 20;
        }

        switch (attackState)
        {
            case AttackState.Idle:
                InstantiatePhysicalAttack(physicalAttack1, attackPosition, attackRotation);
                attackState = AttackState.Attack1;
                break;
            case AttackState.Attack1:
                InstantiatePhysicalAttack(physicalAttack2, attackPosition, attackRotation);
                attackState = AttackState.Attack2;
                break;
            case AttackState.Attack2:
                InstantiatePhysicalAttack(physicalAttack3, attackPosition, attackRotation);
                attackState = AttackState.Attack3;
                break;
            case AttackState.Attack3:
                InstantiatePhysicalAttack(physicalAttack1, attackPosition, attackRotation);
                attackState = AttackState.Attack1;
                break;
        }
        coolTime = attackInterval;
    }

    private void InstantiatePhysicalAttack(PhysicalAttackController PhysicalAttack, Vector2 position, Quaternion rotation)
    {
        PhysicalAttackController physicalAttackController = Instantiate(PhysicalAttack, position, rotation);
        physicalAttackController.SetDirection(direction == Direction.Right);
        physicalAttackController.transform.parent = transform;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                hp -= 1;
                Vector2 newGaugeScale = hpGauge.transform.localScale;
                newGaugeScale.x = defaultGaugeWidth * hp / maxHp;
                hpGauge.transform.localScale = newGaugeScale;
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

                if (hp <= 0)
                {
                    hp = maxHp;
                    newGaugeScale.x = defaultGaugeWidth * hp / maxHp;
                    hpGauge.transform.localScale = newGaugeScale;

                    gameObject.transform.localPosition = new Vector2(-1000.0f, 300.0f);
                }
                break;
            case "MapObject":
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
