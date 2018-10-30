using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    public enum State { Init = 1, Idle = 2, FadeOut = 3 };
    State state;
    Toggle rtaModeToggle;

    FadePanelController fadePanelController;

    // Use this for initialization
    void Start()
    {
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        rtaModeToggle = GameObject.Find("RTAModeToggle").GetComponent<Toggle>();
        state = State.Init;
        Init();
    }

    public void Init()
    {
        fadePanelController.FadeIn();
        state = State.Idle;
        StaticController.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Idle && Input.GetKeyDown("c"))
        {
            fadePanelController.FadeOut("Stage1");
        }
    }

    public void ChangeRTAMode()
    {
        Debug.Log(rtaModeToggle.isOn);
        StaticController.SetSkipAllPrologue(rtaModeToggle.isOn);
    }
}
