using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticController : MonoBehaviour
{
    public static int stageNumber = 0;
    public static float stage1Time = 0;
    public static float stage2Time = 0;
    public static float stage3Time = 0;
    public static string stage1TimeText = "--:--:--";
    public static string stage2TimeText = "--:--:--";
    public static string stage3TimeText = "--:--:--";
    public static bool skipForDebug = false;
    public static bool isSkipStage1Prologue = false;
    public static bool isSkipStage2Prologue = false;
    public static bool isSkipStage3Prologue = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
        {
            skipForDebug = true;
        }
    }

    public static void SetClearTime(float time)
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

    public static void SetClearTimeText(string time)
    {
        switch (stageNumber)
        {
            case 1:
                stage1TimeText = time;
                break;
            case 2:
                stage2TimeText = time;
                break;
            case 3:
                stage3TimeText = time;
                break;
            default:
                break;
        }
    }

    public static void SetStageNumber(int num)
    {
        stageNumber = num;
    }

    public static void SetSkipPrologue()
    {
        switch (stageNumber)
        {
            case 1:
                isSkipStage1Prologue = true;
                break;
            case 2:
                isSkipStage2Prologue = true;
                break;
            case 3:
                isSkipStage3Prologue = true;
                break;
            default:
                break;
        }
    }

    public static bool IsSkipPrologue()
    {
        switch (stageNumber)
        {
            case 1:
                return isSkipStage1Prologue;
            case 2:
                return isSkipStage2Prologue;
            case 3:
                return isSkipStage3Prologue;
            default:
                return false;
        }
    }

    public static string GetTotalTimeText()
    {
        float playTime = stage1Time + stage2Time + stage3Time;
        float playTimeSec = Mathf.Floor(playTime);
        float min = ((int)playTimeSec) / 60;
        float sec = ((int)playTimeSec) % 60;
        float fewSec = Mathf.Floor((playTime - playTimeSec) * 100);

        string minStr = (min < 10.0f ? "0" + min.ToString() : min.ToString());
        string secStr = (sec < 10.0f ? "0" + sec.ToString() : sec.ToString());
        string fewSecStr = (fewSec < 10.0f ? "0" + fewSec.ToString() : fewSec.ToString());

        return minStr + ":" + secStr + ":" + fewSecStr;
    }
}
