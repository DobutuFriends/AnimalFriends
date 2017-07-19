using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakBlock : MonoBehaviour {

    Rigidbody2D rb;

    /// 開始。コルーチンで処理を行う
    //IEnumerator Start()
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        // 移動方向と速さをランダムに決める
        float dir = Random.Range(180, 360);
        float spd = Random.Range(400.0f, 700.0f);
        SetVelocity(dir, spd);

        //// 見えなくなるまで小さくする
        //while (ScaleX > 0.01f)
        //{
        //    // 0.01秒ゲームループに制御を返す
        //    yield return new WaitForSeconds(0.01f);
        //    // だんだん小さくする
        //    MulScale(0.9f);
        //    // だんだん減速する
        //    MulVelocity(1f);
        //}
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Destroy(gameObject);
    }

    public void SetVelocity(float direction, float speed)
    {
        Vector2 v;
        v.x = Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
        v.y = Mathf.Sin(Mathf.Deg2Rad * direction) * speed;
        rb.velocity = v;
    }

    public void MulScale(float d)
    {
        transform.localScale *= d;
    }

    public void MulVelocity(float d)
    {
        rb.velocity *= d;
    }

    public float ScaleX
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.x = value;
            transform.localScale = scale;
        }
        get { return transform.localScale.x; }
    }
}
