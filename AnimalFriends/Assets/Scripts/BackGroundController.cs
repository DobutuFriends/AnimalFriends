using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{

    public float resetPositionX;
    public float speed;
    private GameObject player;
    private Vector3 playerPos;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float XPos = this.gameObject.transform.localPosition.x;
        float YPos = this.gameObject.transform.localPosition.y;
        float ZPos = this.gameObject.transform.localPosition.z;

        Vector3 newPlayerPos = player.transform.localPosition;

        XPos -= (newPlayerPos.x - playerPos.x) * speed;
        YPos -= (newPlayerPos.y - playerPos.y);

        if (this.gameObject.transform.localPosition.x < -resetPositionX)
        {
            XPos += resetPositionX * 2;
        }
        else if (this.gameObject.transform.localPosition.x > resetPositionX)
        {
            XPos -= resetPositionX * 2;
        }

        playerPos = newPlayerPos;
        this.gameObject.transform.localPosition = new Vector3(XPos, YPos, ZPos);
    }
}
