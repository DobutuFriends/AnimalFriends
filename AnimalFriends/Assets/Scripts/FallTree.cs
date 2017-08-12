using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTree : MonoBehaviour {

    Rigidbody2D rd;
    public int maxHp;
    int hp;

	// Use this for initialization
	void Start () {
        rd = gameObject.GetComponent<Rigidbody2D>();
        hp = maxHp;
        rd.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {


	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "PlayerAttack":
                hp -= 1;
                if (hp <= 0)
                {
                    rd.isKinematic = false;
                }
                break;
        }
    }
}
