using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMarkerController : MonoBehaviour
{
    enum MoveType { None = 0, MoveOn = 1, Return = 2, }
    MoveType moveType;
    GameObject goal;
    Vector3 defaultPosition;
    float moveX;
    float moveY;

    // Use this for initialization
    void Start()
    {
        goal = GameObject.Find("Goal");
        defaultPosition = this.transform.position;
        moveX = 0;
        moveY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;

        moveX = 0;
        moveY = 0;

        Vector3 targetPositon;

        if (moveType == MoveType.MoveOn)
        {
            targetPositon = goal.transform.position;
        }
        else if (moveType == MoveType.Return)
        {
            targetPositon = defaultPosition;
        }
        else
        {
            return;
        }

        moveX = getMoveDistsnce(targetPositon.x - x);
        moveY = getMoveDistsnce(targetPositon.y - y);

        if (!GetIsMoving())
        {
            moveType = MoveType.None;
        }

        this.transform.position = new Vector3(x + moveX, y + moveY, z);
    }

    private float getMoveDistsnce(float distance)
    {

        if (distance > 70)
        {
            return 70;
        }
        else if (distance < -70)
        {
            return -70;
        }
        else
        {
            return distance;
        }
    }

    public void MoveOn()
    {
        moveType = MoveType.MoveOn;
    }

    public void Return()
    {
        moveType = MoveType.Return;
    }

    public bool GetIsMoving()
    {
        return (moveX > 1.0f || moveY > 1.0f || moveX < -1.0f || moveY < -1.0f);
    }
}
