using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisControl : MonoBehaviour {

    public Vector2 playerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        playerPosition = GameObject.Find("rabit").transform.position;

        transform.position = playerPosition;
		
	}
}
