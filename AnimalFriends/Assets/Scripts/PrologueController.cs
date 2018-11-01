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
    enum PrologueState2 { Start, Talk1, Talk2, Talk3, MoveOnCamera, Talk4, ReturnCamera, Talk5, Talk6, Talk7, Talk8, Talk9, Talk10, Talk11 };
    enum PrologueState3 { Start, Talk1, Talk2, Talk3, Talk3_2, MoveOnCamera, Talk4, ReturnCamera, Talk5, Talk6, Talk7, Talk8, Talk9, Talk10, Talk11 };
    PrologueState state;
    PrologueState2 state2;
    PrologueState3 state3;
    NPCController npcController;
    CameraMarkerController cameraMarkerController;
    GameController gameController;
    bool isEndTalking = false;

    TextController textControllerLeft, textControllerRight;

    // Use this for initialization
    void Start()
    {
        state = PrologueState.Start;
        state2 = PrologueState2.Start;
        state3 = PrologueState3.Start;
        time = 0;
        pressTime = 0;
        isEndTalking = false;
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        Init();

        if (StaticController.IsSkipPrologue())
        {
            fadePanelController.BlackOut();
        }
        else
        {
            fadePanelController.FadeIn();
        }

        npcController = GameObject.Find("maki").GetComponent<NPCController>();
        cameraMarkerController = GameObject.FindGameObjectWithTag("CameraMarker").GetComponent<CameraMarkerController>();
        gameController = this.GetComponent<GameController>();

        textControllerLeft = GameObject.Find("windowTextLeft").GetComponent<TextController>();
        textControllerRight = GameObject.Find("windowTextRight").GetComponent<TextController>();
    }

    void Init()
    {

        AudioManager.Instance.PlayBGM("bgm_maoudamashii_8bit02", 0.2f, true);

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (IsSkipPrologue())
        {

            switch (StaticController.stageNumber)
            {
                case 0:
                    break;
                case 1:
                    state = PrologueState.Talk9;
                    break;
                case 2:
                    state2 = PrologueState2.Talk9;
                    break;
                case 3:
                    state3 = PrologueState3.Talk9;
                    break;
            }
            isEndTalking = true;
        }

        switch (StaticController.stageNumber)
        {
            case 0:
                break;
            case 1:
                StageOneUpdate();
                break;
            case 2:
                StageTwoUpdate();
                break;
            case 3:
                StageThreeUpdate();
                break;
        }
    }

    private bool IsSkipPrologue()
    {
        if (isEndTalking)
        {
            return false;
        }
        if (StaticController.IsSkipPrologue())
        {
            return true;
        }
        if (Input.GetKey("c"))
        {
            pressTime += Time.deltaTime;

            if (pressTime > 1.0f)
            {
                return true;
            }
        }
        else
        {
            pressTime = 0;
        }
        return false;
    }

    private void StageOneUpdate()
    {
        switch (state)
        {
            case PrologueState.Start:
                if (!fadePanelController.IsFading())
                {
                    npcController.addMoveDict(true, false, false, true, false, 1.1f);
                    textControllerRight.UpdateNewText("ゆかりんゆかりん！   ", TextController.EyeType.Cross, TextController.Priority.Low);
                    state = PrologueState.Talk1;
                }
                break;
            case PrologueState.Talk1:
                if (!textControllerRight.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("どうしたんですか、マキさん   ", TextController.EyeType.Normal, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_1_1", 1.0f);
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
                    textControllerLeft.UpdateNewText("木ですか...？   ", TextController.EyeType.Normal, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_1_2", 1.0f);
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
                    textControllerLeft.UpdateNewText("あれですね      ", TextController.EyeType.Wink, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_1_3", 1.0f);
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
                AudioManager.Instance.PlaySE("prologue_1_4", 1.0f);
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
                    AudioManager.Instance.PlaySE("prologue_1_5", 1.0f);
                    state = PrologueState.Talk9;
                }
                break;
            case PrologueState.Talk9:
                if (!textControllerLeft.GetIsTalking())
                {
                    AudioManager.Instance.FadeOutBGM();
                    cameraMarkerController.Return();
                    fadePanelController.FadeOut();
                    state = PrologueState.Talk10;
                    isEndTalking = true;
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

    private void StageTwoUpdate()
    {
        switch (state2)
        {
            case PrologueState2.Start:
                if (!fadePanelController.IsFading())
                {
                    npcController.addMoveDict(true, false, false, false, true, 1.1f);
                    textControllerRight.UpdateNewText("負けちゃった   ", TextController.EyeType.Cross, TextController.Priority.Low);
                    state2 = PrologueState2.Talk1;
                }
                break;
            case PrologueState2.Talk1:
                if (!textControllerRight.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("今回は私の勝ちですね   ", TextController.EyeType.Smile, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_2_1", 1.0f);
                    state2 = PrologueState2.Talk2;
                }
                break;
            case PrologueState2.Talk2:
                if (!textControllerLeft.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("もう一回！次はあの木にしよう！   ", TextController.EyeType.Anger, TextController.Priority.High);
                    npcController.addMoveDict(true, true, false, false, false, 0.1f);
                    npcController.addMoveDict(false, false, true, false, false, 0.1f);
                    state2 = PrologueState2.Talk3;
                }
                break;
            case PrologueState2.Talk3:
                if (!textControllerRight.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("次の目的地は...   ", TextController.EyeType.Normal, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_2_2", 1.0f);
                    state2 = PrologueState2.MoveOnCamera;
                }
                break;
            case PrologueState2.MoveOnCamera:
                cameraMarkerController.MoveOn();
                state2 = PrologueState2.Talk4;
                break;
            case PrologueState2.Talk4:
                if (!cameraMarkerController.GetIsMoving())
                {
                    textControllerLeft.UpdateNewText("あれですね      ", TextController.EyeType.Wink, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_2_3", 1.0f);
                    state2 = PrologueState2.ReturnCamera;
                }
                break;
            case PrologueState2.ReturnCamera:
                if (!textControllerLeft.GetIsTalking())
                {
                    cameraMarkerController.Return();
                    state2 = PrologueState2.Talk5;
                }
                break;
            case PrologueState2.Talk5:
                textControllerLeft.UpdateNewText("足を踏み外さないように気をつけないといけませんね      ", TextController.EyeType.Normal, TextController.Priority.Low);
                AudioManager.Instance.PlaySE("prologue_2_4", 1.0f);
                state2 = PrologueState2.Talk6;
                break;
            case PrologueState2.Talk6:
                if (!textControllerLeft.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("次は本気でいくからね      ", TextController.EyeType.Normal, TextController.Priority.Low);
                    state2 = PrologueState2.Talk7;
                }
                break;
            case PrologueState2.Talk7:
                if (!cameraMarkerController.GetIsMoving() && !textControllerRight.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("負けないぞ！   ", TextController.EyeType.Smile, TextController.Priority.High);
                    npcController.addMoveDict(true, true, false, false, false, 0.1f);
                    npcController.addMoveDict(false, false, false, true, false, 0.1f);
                    state2 = PrologueState2.Talk8;
                }
                break;
            case PrologueState2.Talk8:
                if (!textControllerRight.GetIsTalking() && !cameraMarkerController.GetIsMoving())
                {
                    textControllerLeft.UpdateNewText("ふふふ、こちらこそ   ", TextController.EyeType.Smile, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_2_5", 1.0f);
                    state2 = PrologueState2.Talk9;
                }
                break;
            case PrologueState2.Talk9:
                if (!textControllerLeft.GetIsTalking())
                {
                    AudioManager.Instance.FadeOutBGM();
                    cameraMarkerController.Return();
                    fadePanelController.FadeOut();
                    state2 = PrologueState2.Talk10;
                    isEndTalking = true;
                }
                break;
            case PrologueState2.Talk10:
                if (!fadePanelController.IsFading())
                {
                    gameController.Init();
                    state2 = PrologueState2.Talk11;
                }
                break;
            case PrologueState2.Talk11:
                break;
        }
    }
    private void StageThreeUpdate()
    {
        switch (state3)
        {
            case PrologueState3.Start:
                if (!fadePanelController.IsFading())
                {
                    npcController.addMoveDict(true, false, false, false, true, 1.1f);
                    textControllerRight.UpdateNewText("いたた、さっきのところで足をくじいちゃった   ", TextController.EyeType.Cross, TextController.Priority.Low);
                    state3 = PrologueState3.Talk1;
                }
                break;
            case PrologueState3.Talk1:
                if (!textControllerRight.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("！ 大丈夫ですか、歩けますか？   ", TextController.EyeType.Normal, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_3_1", 1.0f);
                    state3 = PrologueState3.Talk2;
                }
                break;
            case PrologueState3.Talk2:
                if (!textControllerLeft.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("ちょっと厳しいかも...   ", TextController.EyeType.Normal, TextController.Priority.High);
                    state3 = PrologueState3.Talk3;
                }
                break;
            case PrologueState3.Talk3:
                if (!textControllerRight.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("ここからだと、マキさんのおうちはどっちでしたっけ？   ", TextController.EyeType.Normal, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_3_2", 1.0f);
                    state3 = PrologueState3.Talk3_2;
                }
                break;
            case PrologueState3.Talk3_2:
                if (!textControllerLeft.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("向こうに見える木のそばだよ   ", TextController.EyeType.Normal, TextController.Priority.High);
                    state3 = PrologueState3.MoveOnCamera;
                }
                break;
            case PrologueState3.MoveOnCamera:
                cameraMarkerController.MoveOn();
                state3 = PrologueState3.Talk4;
                break;
            case PrologueState3.Talk4:
                if (!cameraMarkerController.GetIsMoving())
                {
                    textControllerLeft.UpdateNewText("あれですね、わかりました      ", TextController.EyeType.Wink, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_3_3", 1.0f);
                    state3 = PrologueState3.ReturnCamera;
                }
                break;
            case PrologueState3.ReturnCamera:
                if (!textControllerLeft.GetIsTalking())
                {
                    cameraMarkerController.Return();
                    state3 = PrologueState3.Talk5;
                }
                break;
            case PrologueState3.Talk5:
                textControllerLeft.UpdateNewText("おぶっていくので、しっかりつかまっていてください       ", TextController.EyeType.Normal, TextController.Priority.Low);
                AudioManager.Instance.PlaySE("prologue_3_4", 1.0f);
                state3 = PrologueState3.Talk6;
                break;
            case PrologueState3.Talk6:
                if (!textControllerLeft.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("競争の続きはまた今度にしましょう      ", TextController.EyeType.Smile, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_3_5", 1.0f);
                    state3 = PrologueState3.Talk7;
                }
                break;
            case PrologueState3.Talk7:
                if (!textControllerLeft.GetIsTalking())
                {
                    textControllerRight.UpdateNewText("ごめんねゆかりん、ありがとう      ", TextController.EyeType.Normal, TextController.Priority.Low);
                    state3 = PrologueState3.Talk8;
                }
                break;
            case PrologueState3.Talk8:
                if (!cameraMarkerController.GetIsMoving() && !textControllerRight.GetIsTalking())
                {
                    textControllerLeft.UpdateNewText("任せてください   ", TextController.EyeType.Wink, TextController.Priority.Low);
                    AudioManager.Instance.PlaySE("prologue_3_6", 1.0f);
                    state3 = PrologueState3.Talk9;
                }
                break;
            case PrologueState3.Talk9:
                if (!textControllerLeft.GetIsTalking())
                {
                    AudioManager.Instance.FadeOutBGM();
                    cameraMarkerController.Return();
                    fadePanelController.FadeOut();
                    state3 = PrologueState3.Talk10;
                    isEndTalking = true;
                }
                break;
            case PrologueState3.Talk10:
                if (!fadePanelController.IsFading())
                {
                    npcController.SetIsPiggyback(true);
                    npcController.addMoveDict(true, false, false, false, false, 1.1f);
                    gameController.Init();
                    state3 = PrologueState3.Talk11;
                }
                break;
            case PrologueState3.Talk11:
                break;
        }
    }
}
