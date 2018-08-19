using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    Image fadePanelImage;
    bool isPushed;
    float fadeTime;

    // Use this for initialization
    void Start()
    {
        isPushed = false;
        fadeTime = 0;
        fadePanelImage = GameObject.Find("FadePanel").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            isPushed = true;
        }

        if (isPushed)
        {
            float red = fadePanelImage.color.r;
            float green = fadePanelImage.color.g;
            float blue = fadePanelImage.color.b;
            fadeTime += Time.deltaTime;
            fadePanelImage.color = new Color(red, green, blue, fadeTime);
        }

        if (fadeTime > 1.0f)
        {
            SceneManager.LoadScene("PrologueScene");
        }
    }
}
