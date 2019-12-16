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
    private string resultText;
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
        resultText = "";
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
            resultText = "「はっ、私今寝てました？」";
            rank = "E";
        }
        else if (totalScore > 80.0f)
        {
            resultText = "「すみません、ちょっと時間かかっちゃいました」";
            rank = "D";
        }
        else if (totalScore > 60.0f)
        {
            resultText = "「まずまずのスコアですね」";
            rank = "C";
        }
        else if (totalScore > 50.0f)
        {
            resultText = "「だいぶ速かったんじゃないですか？！」";
            rank = "B";
        }
        else if (totalScore > 43.0f)
        {
            resultText = "「ふふっ、韋駄天ゆかりと呼んでください」";
            rank = "A";
        }
        else if (totalScore > 40.0f)
        {
            resultText = "「これが最速と見て間違いないですね」";
            rank = "S";
        }
        else
        {
            resultText = "「え？こんなタイム出るんですか？」";
            rank = "SS";
        }
        rankText.text = rank;
    }

    // Update is called once per frame
    void Update()
    {
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

    public void GoToTitle()
    {
        if (state != State.FadeOut)
        {
            fadePanelController.FadeOut("TitleScene");
            AudioManager.Instance.PlaySE("decision27", 0.3f);
            AudioManager.Instance.FadeOutBGM();
            state = State.FadeOut;
        }
    }
    public void Tweet()
    {
        if (state != State.FadeOut)
        {
            string url = "https://twitter.com/intent/tweet?text=" +
                WWW.EscapeURL("トータルスコア" + StaticController.GetTotalTimeText() + "でランク" + rank + "達成！\n" + resultText + " #ゆかマキ徒競走\nhttps://musasin.github.io/yukamaki.html");
#if UNITY_EDITOR
            Application.OpenURL(url);
#elif UNITY_WEBGL
                Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
#else
                Application.OpenURL(url);
#endif
        }
    }
}
