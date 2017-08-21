using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCheck : MonoBehaviour {

    BossDoor bd;

    // Use this for initialization
    void Start()
    {
        bd = GetComponentInParent<BossDoor>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            bd.EnterFlagRight = true;
        }
    }
}
