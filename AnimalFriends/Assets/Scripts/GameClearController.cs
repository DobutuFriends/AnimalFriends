using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameClearController : MonoBehaviour
{

    public enum State { Init = 1, Idle = 2, FadeOut = 3 };
    State state;
    private Text score1Text;
    private Text score2Text;
    private Text score3Text;
    private Text scoreTotalText;
    private Text rankText;
    private float totalScore;
    private string rank;

    FadePanelController fadePanelController;

    // Use this for initialization
    void Start()
    {
        score1Text = GameObject.Find("Score1").GetComponent<Text>();
        score2Text = GameObject.Find("Score2").GetComponent<Text>();
        score3Text = GameObject.Find("Score3").GetComponent<Text>();
        scoreTotalText = GameObject.Find("ScoreTotal").GetComponent<Text>();
        rankText = GameObject.Find("Rank").GetComponent<Text>();
        score1Text.text = StaticController.stage1TimeText;
        score2Text.text = StaticController.stage2TimeText;
        score3Text.text = StaticController.stage3TimeText;
        scoreTotalText.text = StaticController.GetTotalTimeText();
        totalScore = StaticController.stage1Time + StaticController.stage2Time + StaticController.stage3Time;
        scoreTotalText = GameObject.Find("ScoreTotal").GetComponent<Text>();
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        state = State.Init;
        Init();
    }

    public void Init()
    {
        AudioManager.Instance.PlayBGM("bgm_maoudamashii_8bit13", 0.15f, true);
        fadePanelController.FadeIn();
        state = State.Idle;

        if (totalScore > 180.0f)
        {
            rank = "E";
        }
        else if (totalScore > 80.0f)
        {
            rank = "D";
        }
        else if (totalScore > 60.0f)
        {
            rank = "C";
        }
        else if (totalScore > 50.0f)
        {
            rank = "B";
        }
        else if (totalScore > 43.0f)
        {
            rank = "A";
        }
        else if (totalScore > 40.0f)
        {
            rank = "S";
        }
        else
        {
            rank = "SS";
        }
        rankText.text = rank;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Idle && Input.GetKeyDown("c"))
        {
            AudioManager.Instance.FadeOutBGM();
            fadePanelController.FadeOut("TitleScene");
            state = State.FadeOut;
        }
    }

    public void PlayGameClearSound()
    {
        AudioManager.Instance.PlaySE("game_clear", 1.0f);
    }
    public void PlayGameClear2Sound()
    {
        AudioManager.Instance.PlaySE("game_clear_2", 1.0f);
    }
    public void PlayGameClear3Sound()
    {
        switch (rank)
        {
            case "E":
                AudioManager.Instance.PlaySE("clear_voice_1", 1.0f);
                break;
            case "D":
                AudioManager.Instance.PlaySE("clear_voice_2", 1.0f);
                break;
            case "C":
                AudioManager.Instance.PlaySE("clear_voice_3", 1.0f);
                break;
            case "B":
                AudioManager.Instance.PlaySE("clear_voice_4", 1.0f);
                break;
            case "A":
                AudioManager.Instance.PlaySE("clear_voice_5", 1.0f);
                break;
            case "S":
                AudioManager.Instance.PlaySE("clear_voice_6", 1.0f);
                break;
            case "SS":
                AudioManager.Instance.PlaySE("clear_voice_7", 1.0f);
                break;
        }


    }
}
