using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCheck : MonoBehaviour {

    BossDoor bd;

	// Use this for initialization
	void Start () {
        bd = GetComponentInParent<BossDoor>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bd.EnterFlagLeft = true;
    }
}
