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
        mode = Mode.Stop;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 position = transform.position;
        Vector2 scale = transform.localScale;

        switch (mode)
        {
            case Mode.Stop:
                // とりあえず左に動かす
                mode = Mode.MoveLeft;
                break;

            case Mode.MoveLeft:
                position.x -= speed;
                scale.x = 1;
                break;

            case Mode.MoveRight:
                position.x += speed;
                scale.x = -1;
                break;

            default:
                break;
        }

        transform.position = position;
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
