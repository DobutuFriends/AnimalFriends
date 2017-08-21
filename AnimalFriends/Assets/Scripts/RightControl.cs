using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControl : MonoBehaviour {

    BoxCollider2D bc;
    GimmickDoorControl GDcontrol;

    // Use this for initialization
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        GDcontrol = GetComponentInParent<GimmickDoorControl>();
        setEnable();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GDcontrol.left = false;
        GDcontrol.setEnable(0);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GDcontrol.right = true;
        setEnable();
    }

    public void setEnable()
    {
        if (GDcontrol.right == true) { bc.enabled = true; }
        else { bc.enabled = false; }
    }
}
