using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakOnGround : MonoBehaviour {

    /// <summary>
    /// 地面の破壊までの時間
    /// </summary>
    public float TIME_OUT;

    /// <summary>
    /// 時間計測用変数
    /// </summary>
    protected float timeElapsed;

    bool onGround;

	// Use this for initialization
	void Start () {
        onGround = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (onGround == true)
        {
            timeElapsed += Time.deltaTime;
            Debug.Log(timeElapsed);
            if (TIME_OUT <= timeElapsed)
            {
                destroyObj();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        onGround = true;
    }

    void destroyObj()
    {
        Destroy(gameObject);
    }

}
