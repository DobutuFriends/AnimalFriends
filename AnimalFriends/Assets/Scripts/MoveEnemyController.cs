using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    public int maxHp;
    int hp;
    GameObject hpGauge;
    float defaultGaugeWidth;

    enum Mode { Stop, MoveLeft, MoveRight, KnockBack, };
    enum Direction { Right, Left, };
    Mode mode;
    Direction direction;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        hpGauge = gameObject.transform.Find("hpGauge").gameObject;
        defaultGaugeWidth = hpGauge.transform.localScale.x;
        mode = Mode.Stop;
        hp = maxHp;
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
                direction = Direction.Left;
                break;

            case Mode.MoveRight:
                velocityX = speed;
                scale.x = -1;
                direction = Direction.Right;
                break;

            default:
                velocityX = rb.velocity.x;
                break;
        }

        rb.velocity = new Vector2(velocityX, velocityY);
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                break;
            case "MapObject":
            case "Enemy":
                float footPosition = transform.position.y + col.offset.y - (col.size.y / 2);
                foreach (ContactPoint2D point in collision.contacts)
                {
                    // 足元のオブジェクトは無視する
                    if (point.point.y >= footPosition)
                    {
                        ChangeMoveType();
                        break;
                    }
                }
                if (mode == Mode.KnockBack)
                {
                    mode = (direction == Direction.Right ? Mode.MoveRight : Mode.MoveLeft);
                }
                break;
            case "PlayerAttack":
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                break;
            case "MapObject":
                break;
            case "PlayerAttack":
                hp -= 1;
                Vector2 newGaugeScale = hpGauge.transform.localScale;
                newGaugeScale.x = defaultGaugeWidth * hp / maxHp;
                hpGauge.transform.localScale = newGaugeScale;

                if (transform.localPosition.x < collision.transform.localPosition.x)
                {
                    rb.velocity = new Vector2(-500.0f, 500.0f);
                }
                else
                {
                    rb.velocity = new Vector2(500.0f, 500.0f);
                }
                mode = Mode.KnockBack;

                if (hp <= 0)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void ChangeMoveType()
    {
        switch (mode)
        {
            case Mode.MoveLeft:
                mode = Mode.MoveRight;
                direction = Direction.Right;
                break;
            case Mode.MoveRight:
                mode = Mode.MoveLeft;
                direction = Direction.Left;
                break;
            default:
                break;
        }
    }
}
