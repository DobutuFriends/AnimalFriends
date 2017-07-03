using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpPower;
    private Rigidbody2D rb;
    private Animator animator;
    enum State { Idle = 0, Walk = 1, JumpUp = 2, JumpDown = 3 };
    State state;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newVelocity = Move();
        state = CalcState(newVelocity.x, newVelocity.y);
        animator.SetInteger("state", (int)state);
        Debug.Log(state);
    }

    Vector2 Move()
    {
        Vector2 scale = transform.localScale;
        float velocityX = 0;
        float velocityY = rb.velocity.y;

        if (Input.GetKey("left"))
        {
            velocityX = -speed;
            scale.x = -1;
        }
        else if (Input.GetKey("right"))
        {
            velocityX = speed;
            scale.x = 1;
        }
        else
        {
            velocityX = 0;
        }

        if (Input.GetKey("up"))
        {
            velocityY = jumpPower;
        }

        transform.localScale = scale;

        Vector2 newVelocity = new Vector2(velocityX, velocityY);
        rb.velocity = newVelocity;
        return newVelocity;
    }

    State CalcState(float velocityX, float velocityY)
    {
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
}
