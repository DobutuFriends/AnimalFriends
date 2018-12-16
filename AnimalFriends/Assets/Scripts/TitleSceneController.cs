using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    public enum State { Init = 1, Opening = 2, Idle = 3, FadeOut = 4 };
    State state;
    Toggle rtaModeToggle;

    FadePanelController fadePanelController;

    // Use this for initialization
    void Start()
    {
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        rtaModeToggle = GameObject.Find("RTAModeToggle").GetComponent<Toggle>();
        rtaModeToggle.isOn = StaticController.isSkipAllPrologue;
        state = State.Init;
        Init();
    }

    public void Init()
    {
        fadePanelController.FadeIn();
        state = State.Opening;
        StaticController.Reset();
        AudioManager.Instance.PlayBGM("game_maoudamashii_7_event37", 0.2f, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Opening && !fadePanelController.IsFading())
        {
            AudioManager.Instance.PlaySE("title", 1.0f);
            AudioManager.Instance.PlaySE("maki_title", 0.6f);
            state = State.Idle;
        }
        if (state == State.Idle && Input.GetKeyDown("c"))
        {
            AudioManager.Instance.FadeOutBGM();
            fadePanelController.FadeOut("Stage1");
            AudioManager.Instance.PlaySE("decision27", 0.3f);
            state = State.FadeOut;
        }
    }

    public void ChangeRTAMode()
    {
        Debug.Log(rtaModeToggle.isOn);
        StaticController.SetSkipAllPrologue(rtaModeToggle.isOn);
    }
}
