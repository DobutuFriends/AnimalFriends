using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftControl : MonoBehaviour {

    BoxCollider2D bc;
    GimmickDoorControl GDcontrol;

	// Use this for initialization
	void Start () {
        bc = GetComponent<BoxCollider2D>();
        GDcontrol = GetComponentInParent<GimmickDoorControl>();
        setEnable();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GDcontrol.right = false;
        GDcontrol.setEnable(1);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GDcontrol.left = true;
        setEnable();
    }

    public void setEnable()
    {
        if (GDcontrol.left == true) { bc.enabled = true; }
        else { bc.enabled = false; }
    }
    
}
