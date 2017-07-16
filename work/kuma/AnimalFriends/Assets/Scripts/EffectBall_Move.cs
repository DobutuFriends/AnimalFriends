using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unity.Linq;

public class EffectBall : MonoBehaviour {

    public float SPEED;
    public float RADIUS;
    public float numberRadius;
    public string objName = "EeffectBall_";

    public Vector2 position;
    public Vector2 playerPosition;

    public static GameObject[] gObj = new GameObject[4];
    public static bool canEBall = true;
    public static bool attackFlag = false;


	// Use this for initialization
	void Start () {

        SPEED = 4f;

        if (canEBall == true)
        {
            initEBall();
        }

        if (gameObject.name == objName + "1")
        {
            numberRadius = 300;
        }
        else if (gameObject.name == objName + "2")
        {
            numberRadius = 600;
        }
        else if (gameObject.name == objName + "3")
        {
            numberRadius = 900;
        }
        else
        {
            numberRadius = 0;
        }

        canEBall = false;
		
	}
	
	void Update () 
    {
        Debug.Log(attackFlag);

        if (attackFlag == true)
        {
            attackEBall();
        }
        else if (attackFlag == false)
        {
            moveCircle();
        }
        
	}

    void moveCircle()
    {
        // 経過時間の取得
        float time = Time.time;
        // 円運動の座標演算
        float x = Mathf.Sin(time * SPEED + numberRadius) * (RADIUS);
        float y = Mathf.Cos(time * SPEED + numberRadius) * (RADIUS);
        // オブジェクトに座標を代入
        transform.localPosition = new Vector2(-x, y);
    }

    void attackEBall()
    {
        position = transform.position;

        position.x += Mathf.Cos(1f);

        transform.position = position;

    }

    void initEBall()
    {
        for (int i = 0; i <= 3; i++)
        {
            Debug.Log(i);
            gObj[i] = GameObject.Find(objName + i);
            if (gObj[i].activeSelf == true)
            {
                gObj[i].SetActive(false);
            }
        }
    }
}
