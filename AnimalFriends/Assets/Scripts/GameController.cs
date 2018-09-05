using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum State { Idle = 1, Init = 2, CountDown = 3, Start = 4, Playing = 5, PlayerGoal = 6, ClearWait = 7, FadeOut = 8 };
    State state;

    FadePanelController fadePanelController;
    CountNumberController countNumberController;
    NPCController npcController;
    PlayerController playerController;
    TextController textControllerLeft, textControllerRight;
    private Text timeText;
    bool isPlayingGame = false;
    float idleTime = 0;
    float playTime = 0;

    // Use this for initialization
    void Start()
    {
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        countNumberController = GameObject.Find("CountNumber").GetComponent<CountNumberController>();
        npcController = GameObject.Find("maki").GetComponent<NPCController>();
        playerController = GameObject.Find("yukari").GetComponent<PlayerController>();
        textControllerLeft = GameObject.Find("windowTextLeft").GetComponent<TextController>();
        textControllerRight = GameObject.Find("windowTextRight").GetComponent<TextController>();
        timeText = GameObject.Find("Time").GetComponent<Text>();
        state = State.Idle;
    }

    public void Init()
    {
        StaticController.stageNumber = 1;
        fadePanelController.FadeIn();
        playerController.SetPosition(new Vector3(-75, 18, 0));
        playerController.SetScale(new Vector3(1, 1, 1));
        npcController.SetPosition(new Vector3(250, 18, 0));
        npcController.SetScale(new Vector3(1, 1, 1));
        state = State.Init;
    }

    // Update is called once per frame
    void Update()
    {
        idleTime += Time.deltaTime;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Init:
                if (!fadePanelController.IsFading())
                {
                    state = State.CountDown;
                    countNumberController.SetIsPlaying(true);
                    idleTime = 0;
                }
                break;
            case State.CountDown:
                if (!countNumberController.IsPlayingAnimation() && idleTime > 1.0f)
                {
                    state = State.Start;
                }
                break;
            case State.Start:
                playerController.SetCanMove(true);
                npcController.addMoveDict(false, false, true, false, false, 2.0f);
                textControllerRight.UpdateNewText("ギュンギュンいくよー！", TextController.EyeType.Cross, TextController.Priority.Normal);
                state = State.Playing;
                break;
            case State.Playing:
                playTime += Time.deltaTime;

                float playTimeSec = Mathf.Floor(playTime);
                float min = ((int)playTimeSec) / 60;
                float sec = ((int)playTimeSec) % 60;
                float fewSec = Mathf.Floor((playTime - playTimeSec) * 100);

                string minStr = (min < 10.0f ? "0" + min.ToString() : min.ToString());
                string secStr = (sec < 10.0f ? "0" + sec.ToString() : sec.ToString());
                string fewSecStr = (fewSec < 10.0f ? "0" + fewSec.ToString() : fewSec.ToString());

                timeText.text = minStr + ":" + secStr + ":" + fewSecStr;

                break;
            case State.PlayerGoal:
                idleTime = 0;
                playerController.SetCanMove(false);
                state = State.ClearWait;
                StaticController.setClearTime(playTime);
                break;
            case State.ClearWait:
                if (idleTime > 2.0f)
                {
                    fadePanelController.FadeOut("MainScene");
                    state = State.FadeOut;
                }
                break;
            case State.FadeOut:
                break;
        }
    }

    public void PlayerGoal()
    {
        state = State.PlayerGoal;
    }
}
