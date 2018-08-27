using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum State { Idle = 1, Init = 2, CountDown = 3, Start = 4, Playing = 5, Hoge = 6, };
    State state;

    FadePanelController fadePanelController;
    CountNumberController countNumberController;
    NPCController npcController;
    PlayerController playerController;
    TextController textControllerLeft, textControllerRight;
    bool isPlayingGame = false;
    float idleTime = 0;

    // Use this for initialization
    void Start()
    {
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        countNumberController = GameObject.Find("CountNumber").GetComponent<CountNumberController>();
        npcController = GameObject.Find("maki").GetComponent<NPCController>();
        playerController = GameObject.Find("yukari").GetComponent<PlayerController>();
        textControllerLeft = GameObject.Find("windowTextLeft").GetComponent<TextController>();
        textControllerRight = GameObject.Find("windowTextRight").GetComponent<TextController>();
        state = State.Idle;
    }

    public void Init()
    {
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
                break;
            case State.Hoge:
                break;
        }
    }
}
