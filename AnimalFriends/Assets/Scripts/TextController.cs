using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextController : MonoBehaviour
{

    private Animator standYukariAnimator;
    private Text windowText;
    string newText;
    int textIndex;

    float lastUpdateTime;
    float addTextInterval;
    float time;

    bool isTalking;

    // Use this for initialization
    void Start()
    {
        isTalking = false;
        windowText = this.GetComponent<Text>();
        standYukariAnimator = GameObject.Find("stand_yukari").GetComponent<Animator>();

        windowText.text = "";
        newText = "１２３４５６７８９０１２３４５６７８９０１２\n横幅全角２２文字まで入ります入りますそれ以上は自動改行で、合計３行まで入ります入ります入ります入ります";
        textIndex = 0;
        addTextInterval = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isTalking);
        time += Time.deltaTime;
        if (textIndex < newText.Length)
        {
            isTalking = true;
            if (lastUpdateTime + addTextInterval < time)
            {
                windowText.text += newText[textIndex];
                textIndex++;
                lastUpdateTime = time;
            }
        }
        else
        {
            isTalking = false;
        }
        standYukariAnimator.SetBool("isTalking", isTalking);
    }

    public void UpdateNewText(string newText, float addTextInterval = 0.1f)
    {
        isTalking = true;
        this.newText = newText;
        this.addTextInterval = addTextInterval;
        windowText.text = "";
        textIndex = 0;
    }
}
