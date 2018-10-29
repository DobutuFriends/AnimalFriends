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

    FadePanelController fadePanelController;

    // Use this for initialization
    void Start()
    {
        score1Text = GameObject.Find("Score1").GetComponent<Text>();
        score2Text = GameObject.Find("Score2").GetComponent<Text>();
        score3Text = GameObject.Find("Score3").GetComponent<Text>();
        scoreTotalText = GameObject.Find("ScoreTotal").GetComponent<Text>();
        score1Text.text = StaticController.stage1TimeText;
        score2Text.text = StaticController.stage2TimeText;
        score3Text.text = StaticController.stage3TimeText;
        scoreTotalText.text = StaticController.GetTotalTimeText();
        scoreTotalText = GameObject.Find("ScoreTotal").GetComponent<Text>();
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        state = State.Init;
        Init();
    }

    public void Init()
    {
        fadePanelController.FadeIn();
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Idle && Input.GetKeyDown("c"))
        {
            fadePanelController.FadeOut("TitleScene");
            state = State.FadeOut;
        }
    }

    public void PlayGameClearSound()
    {
        AudioManager.Instance.PlaySE("game_clear", 0.3f);
    }
    public void PlayGameClear2Sound()
    {
        AudioManager.Instance.PlaySE("game_clear_2", 0.3f);
    }
    public void PlayGameClear3Sound()
    {
        AudioManager.Instance.PlaySE("clear_voice_7", 0.3f);
    }
}
