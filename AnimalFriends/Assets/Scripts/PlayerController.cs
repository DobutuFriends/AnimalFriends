using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpPower;
    private int isRunningId = Animator.StringToHash("isRunning");
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (Input.GetKey("left"))
        {
            position.x -= speed;
            scale.x = -1;
        }
        else if (Input.GetKey("right"))
        {
            position.x += speed;
            scale.x = 1;
        }

        if (Input.GetKey("up"))
        {
            rb.velocity = new Vector2(0, jumpPower);
        }

        transform.position = position;
        transform.localScale = scale;
    }
}
