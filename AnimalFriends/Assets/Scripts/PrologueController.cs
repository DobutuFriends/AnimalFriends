using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueController : MonoBehaviour
{
    float time;
    float pressTime;
    FadePanelController fadePanelController;
    enum PrologueState { Start, Talk1, Talk2, Talk3, MoveOnCamera, Talk4, ReturnCamera, Talk5, Talk6, Talk7, Talk8, Talk9, Talk10, Talk11 };
    PrologueState state;
    NPCController npcController;
    CameraMarkerController cameraMarkerController;
    GameController gameController;

    TextController textControllerLeft, textControllerRight;

    // Use this for initialization
    void Start()
    {
        state = PrologueState.Start;
        time = 0;
        pressTime = 0;
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        fadePanelController.FadeIn();

        npcController = GameObject.Find("maki").GetComponent<NPCController>();
        cameraMarkerController = GameObject.FindGameObjectWithTag("CameraMarker").GetComponent<CameraMarkerController>();
        gameController = this.GetComponent<GameController>();

        textControllerLeft = GameObject.Find("windowTextLeft").GetComponent<TextController>();
        textControllerRight = GameObject.Find("windowTextRight").GetComponent<TextController>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKey("c"))
        {
            pressTime += Time.deltaTime;

            if (pressTime > 0.5f && state != PrologueState.Talk10 && state != PrologueState.Talk11)
            {
                state = PrologueState.Talk9;
            }

        }
        else
        {
            pressTime = 0;
        }

        switch (state)
        {
            case PrologueState.Start:
                npcController.addMoveDict(true, false, false, true, false, 1.1f);
                textControllerRight.UpdateNewText("ゆかりんゆかりん！   ", TextController.EyeType.Cross, TextController.Priority.Low);
                state = PrologueState.Talk1;
                break;
            case PrologueState.Talk1:
                if (!textControllerRight.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("どうしたんですか、マキさん   ", TextController.EyeType.Normal, TextController.Priority.Low);
                    state = PrologueState.Talk2;
                }
                break;
            case PrologueState.Talk2:
                if (!textControllerLeft.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("向こうに大きな木があったんだ！   ", TextController.EyeType.Star, TextController.Priority.High);
                    npcController.addMoveDict(true, true, false, false, false, 0.1f);
                    npcController.addMoveDict(false, false, true, false, false, 0.1f);
                    state = PrologueState.Talk3;
                }
                break;
            case PrologueState.Talk3:
                if (!textControllerRight.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("木...？   ", TextController.EyeType.Normal, TextController.Priority.Low);
                    state = PrologueState.MoveOnCamera;
                }
                break;
            case PrologueState.MoveOnCamera:
                cameraMarkerController.MoveOn();
                state = PrologueState.Talk4;
                break;
            case PrologueState.Talk4:
                if (!cameraMarkerController.GetIsMoving())
                {
                    textControllerLeft.UpdateNewText("あれですか      ", TextController.EyeType.Wink, TextController.Priority.Low);
                    state = PrologueState.ReturnCamera;
                }
                break;
            case PrologueState.ReturnCamera:
                if (!textControllerLeft.GetIsTalking())
                {
                    cameraMarkerController.Return();
                    state = PrologueState.Talk5;
                }
                break;
            case PrologueState.Talk5:
                textControllerLeft.UpdateNewText("ここからでもよく見えますね      ", TextController.EyeType.Normal, TextController.Priority.Low);
                state = PrologueState.Talk6;
                break;
            case PrologueState.Talk6:
                if (!textControllerLeft.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("目印にちょうど良いと思ったんだ      ", TextController.EyeType.Normal, TextController.Priority.Low);
                    state = PrologueState.Talk7;
                }
                break;
            case PrologueState.Talk7:
                if (!cameraMarkerController.GetIsMoving() && !textControllerRight.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("あそこまで競争しようよ！   ", TextController.EyeType.Smile, TextController.Priority.High);
                    npcController.addMoveDict(true, true, false, false, false, 0.1f);
                    npcController.addMoveDict(false, false, false, true, false, 0.1f);
                    state = PrologueState.Talk8;
                }
                break;
            case PrologueState.Talk8:
                if (!textControllerRight.GetIsTalking() && !cameraMarkerController.GetIsMoving())
                {
                    textControllerLeft.UpdateNewText("いいですよ   ", TextController.EyeType.Smile, TextController.Priority.Low);
                    state = PrologueState.Talk9;
                }
                break;
            case PrologueState.Talk9:
                if (!textControllerLeft.GetIsTalking())
                {
                    cameraMarkerController.Return();
                    fadePanelController.FadeOut();
                    state = PrologueState.Talk10;
                }
                break;
            case PrologueState.Talk10:
                if (!fadePanelController.IsFading())
                {
                    gameController.Init();
                    state = PrologueState.Talk11;
                }
                break;
            case PrologueState.Talk11:
                break;
        }
    }
}
