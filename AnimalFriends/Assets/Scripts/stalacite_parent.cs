using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalacite_parent : MonoBehaviour {


    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        if (other.gameObject.tag == "Player")
        {
            rb.gravityScale = 200;
        }
    }
}
