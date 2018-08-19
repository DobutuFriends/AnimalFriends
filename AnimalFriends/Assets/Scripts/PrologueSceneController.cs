using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueSceneController : MonoBehaviour
{

    Image fadePanelImage;
    float fadeTime;

    // Use this for initialization
    void Start()
    {

        fadeTime = 0;
        fadePanelImage = GameObject.Find("FadePanel").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTime < 2.0f)
        {
            float red = fadePanelImage.color.r;
            float green = fadePanelImage.color.g;
            float blue = fadePanelImage.color.b;
            fadeTime += Time.deltaTime;
            fadePanelImage.color = new Color(red, green, blue, 1.0f - fadeTime);
        }
    }
}
