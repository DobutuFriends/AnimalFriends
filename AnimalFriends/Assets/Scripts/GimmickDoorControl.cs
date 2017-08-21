using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickDoorControl : MonoBehaviour {

    public bool right;
    public bool left;

    RightControl rightDoor;
    LeftControl leftDoor;

	// Use this for initialization
	void Start () {
        rightDoor = GetComponentInChildren<RightControl>();
        leftDoor = GetComponentInChildren<LeftControl>();
	}
	
    public void setEnable(int num)
    {
        if (num == 0)
        {
            leftDoor.setEnable();
        }
        else if (num == 1)
        {
            rightDoor.setEnable();
        }
  
    }
 
}

