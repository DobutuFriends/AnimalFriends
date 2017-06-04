using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public float SPEED;
	public float JUMP_POWER;
    /// <summary>
    /// ジャンプ継続時間
    /// </summary>
    public float TIME_OUT;
    /// <summary>
    /// "up"キー押下経過時間
    /// </summary>
    protected float timeElapsed;

	protected Rigidbody2D rb;
    protected Vector2 position;
    protected Vector2 scale;
    protected bool jumppingFlag;
    protected bool jumpCanFlag;

    public static Vector2 groundCheck;
    public static Vector2 groundArea;
    public static bool grounded;

    //OverlapAreaで絞る為のレイヤーマスクの変数
    public LayerMask whatIsGround;

    void Start() { }
    void Update() 
    {

    
    }

    /// <summary>
    /// 移動関数
    /// </summary>
	protected void Move()
	{
        position = transform.position;
        scale = transform.localScale;

        moveLR();
        jump();

		transform.position = position;
		transform.localScale = scale;
	}

    /// <summary>
    /// 左右移動
    /// </summary>
    private void moveLR()
    {
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
    }

    /// <summary>
    /// "up"キーを押している時間に応じて上昇
    /// </summary>
    private void jump()
    {
        if (Input.GetKey("up"))
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed <= TIME_OUT)
            {
                jumppingFlag = true;
            }
            else
            {
                jumppingFlag = false;
            }

            if (jumppingFlag == true)
            {
                rb.velocity = new Vector2(0, JUMP_POWER);
            }

            //if (jumpFlag == true && grounded == true)
            //{
            //    rb.velocity = new Vector2(0, JUMP_POWER);
            //    Debug.Log("on the floor");
            //    if (jumpFlag == true && grounded != false && Input.GetKey("up") == true)
            //    {
            //        rb.velocity = new Vector2(0, JUMP_POWER);
            //        Debug.Log("in the sky");
            //    }
            //}
        }
        if (grounded == true)
        {
            timeElapsed = 0.0f;
        }
    }

    /// <summary>
    /// 床の上チェック
    /// </summary>
    protected void checkOnFloor()
    {

        groundCheck = new Vector2(position.x + GetComponent<BoxCollider2D>().offset.x + (GetComponent<BoxCollider2D>().size.y / 4),
            position.y + GetComponent<BoxCollider2D>().offset.y - (GetComponent<BoxCollider2D>().size.y / 2));
        groundArea = new Vector2(position.x + GetComponent<BoxCollider2D>().offset.x - (GetComponent<BoxCollider2D>().size.y / 4), 
            (position.y - GetComponent<BoxCollider2D>().size.y) / 10.0f);

        //あたり判定四角領域の範囲
        grounded = Physics2D.OverlapArea(groundCheck + groundArea , groundCheck - groundArea, whatIsGround);

        if (grounded == true)
        {
            jumpCanFlag = true;
        }
        else if (grounded != false && Input.GetKey("up") == true)
        {
            jumpCanFlag = true;
        }
        else
        {
            jumpCanFlag = false;
        }
    }
}
