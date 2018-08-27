using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{

    public float resetPositionX;
    public float speed;
    private GameObject marker;
    private Vector3 markerPos;

    // Use this for initialization
    void Start()
    {
        marker = GameObject.FindGameObjectWithTag("CameraMarker");
        markerPos = marker.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float XPos = this.gameObject.transform.localPosition.x;
        float YPos = this.gameObject.transform.localPosition.y;
        float ZPos = this.gameObject.transform.localPosition.z;

        Vector3 newmarkerPos = marker.transform.position;
        XPos -= (newmarkerPos.x - markerPos.x) * speed;
        YPos -= (newmarkerPos.y - markerPos.y);

        if (this.gameObject.transform.localPosition.x < -resetPositionX)
        {
            XPos += resetPositionX * 2;
        }
        else if (this.gameObject.transform.localPosition.x > resetPositionX)
        {
            XPos -= resetPositionX * 2;
        }

        markerPos = newmarkerPos;
        this.gameObject.transform.localPosition = new Vector3(XPos, YPos, ZPos);
    }
}
