using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;

    enum Mode { Stop, MoveLeft, MoveRight };
    Mode mode;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mode = Mode.Stop;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 scale = transform.localScale;
        float velocityX = 0;
        float velocityY = rb.velocity.y;

        switch (mode)
        {
            case Mode.Stop:
                velocityX = 0;

                // とりあえず左に動かす
                mode = Mode.MoveLeft;
                break;

            case Mode.MoveLeft:
                velocityX = -speed;
                scale.x = 1;
                break;

            case Mode.MoveRight:
                velocityX = speed;
                scale.x = -1;
                break;

            default:
                velocityX = 0;
                break;
        }

        rb.velocity = new Vector2(velocityX, velocityY);
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // とりあえず何かにぶつかったら逆方向に進む
        switch (mode)
        {
            case Mode.MoveLeft:
                mode = Mode.MoveRight;
                break;
            case Mode.MoveRight:
                mode = Mode.MoveLeft;
                break;
            default:
                break;
        }
    }
}
