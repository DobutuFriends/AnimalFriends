using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public enum State { Init = 1, Idle = 2, FadeOut = 3 };
    State state;

    FadePanelController fadePanelController;

    // Use this for initialization
    void Start()
    {
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
            fadePanelController.FadeOut("Stage" + (StaticController.stageNumber == 0 ? 1 : StaticController.stageNumber));
            StaticController.SetSkipPrologue();
            state = State.FadeOut;
        }
    }
}
