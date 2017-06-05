using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitControl : PlayerControl {
    
    //rabit用アニメーション？
    protected int isRunningId = Animator.StringToHash("isRunning");

	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
    {
        Move();
	}

    void FixedUpdate()
    {
        checkOnFloor();
    }
}
