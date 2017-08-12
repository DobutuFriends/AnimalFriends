using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakOnGround : MonoBehaviour
{

    /// <summary>
    /// 地面の破壊までの時間
    /// </summary>
    public float TIME_OUT;

    /// <summary>
    /// 時間計測用変数
    /// </summary>
    float timeElapsed;

    bool onGround;

    GameObject go;

    [SerializeField]
    string objectName;

    [SerializeField]
    GameObject perticle;

    /// プレハブ
    static GameObject _prefab = null;
    /// パーティクルの生成
    public static void Add(GameObject perticle, float x, float y)
    {
        Vector3 position = new Vector3(x, y, 0);
        Instantiate(perticle, position, Quaternion.identity);
    }

    // Use this for initialization
    void Start()
    {
        go = GameObject.Find(objectName);
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround == true)
        {
            timeElapsed += Time.deltaTime;
            if (TIME_OUT <= timeElapsed)
            {
                // パーティクルを生成
                for (int i = 0; i < 32; i++)
                {
                    Add(perticle, go.transform.localPosition.x, go.transform.localPosition.y);
                }
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            onGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            onGround = false;
        }
    }
}
