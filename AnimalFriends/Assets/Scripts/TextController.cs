using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextController : MonoBehaviour
{

    private Animator standYukariAnimator;
    private Animator eyeAnimator;
    private Animator mouseAnimator;
    private Text windowText;
    string newText;
    int textIndex;

    float lastUpdateTime;
    float addTextInterval;
    float time;

    bool isTalking;

    public enum EyeType { Normal = 1, Smile = 2, Anger = 3, Wink = 4, Cross = 5, };
    private EyeType eyeType = EyeType.Normal;
    public enum Priority { Lowest = 1, Low = 2, Normal = 3, High = 4, Highest = 5, };
    private Priority priority = Priority.Lowest;

    // Use this for initialization
    void Start()
    {
        isTalking = false;
        windowText = this.GetComponent<Text>();
        eyeAnimator = GameObject.Find("stand_yukari_eye").GetComponent<Animator>();
        mouseAnimator = GameObject.Find("stand_yukari_mouse").GetComponent<Animator>();

        windowText.text = "";
        newText = "１２３４５６７８９０１２３４５６７８９０１２\n横幅全角２２文字まで入ります入りますそれ以上は自動改行で、合計３行まで入ります入ります入ります入ります";
        textIndex = 0;
        addTextInterval = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(eyeType);
        mouseAnimator.SetBool("isTalking", isTalking);
        eyeAnimator.SetInteger("eyeType", (int)eyeType);
    }

    public void UpdateNewText(string newText, EyeType eyeType = EyeType.Normal, Priority priority = Priority.Normal, float addTextInterval = 0.1f)
    {
        if (textIndex < this.newText.Length && this.priority > priority)
        {
            return;
        }
        isTalking = true;
        this.newText = newText;
        this.addTextInterval = addTextInterval;
        this.priority = priority;
        this.eyeType = eyeType;
        windowText.text = "";
        textIndex = 0;
    }
}
