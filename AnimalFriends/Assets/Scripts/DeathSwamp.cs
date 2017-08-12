using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSwamp : MonoBehaviour
{
    GameObject go;
    [SerializeField]
    int damage;

    // Use this for initialization
    void Start()
    {
        go = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            go.GetComponent<PlayerController>().Damage(damage);
        }
    }
}
