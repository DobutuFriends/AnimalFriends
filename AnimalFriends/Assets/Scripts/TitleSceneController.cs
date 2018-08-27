using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    FadePanelController fadePanelController;

    // Use this for initialization
    void Start()
    {
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            fadePanelController.FadeOut("MainScene");
        }
    }
}
