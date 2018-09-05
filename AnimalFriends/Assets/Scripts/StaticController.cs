using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticController : MonoBehaviour
{
    public static int stageNumber = 0;
    public static float stage1Time = 0;
    public static float stage2Time = 0;
    public static float stage3Time = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(stageNumber);
        Debug.Log(stage1Time);
    }

    public static void setClearTime(float time)
    {
        switch (stageNumber)
        {
            case 1:
                stage1Time = time;
                break;
            case 2:
                stage2Time = time;
                break;
            case 3:
                stage3Time = time;
                break;
            default:
                break;
        }
    }
}
