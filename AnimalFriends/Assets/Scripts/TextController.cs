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

    public enum EyeType { Normal = 1, Smile = 2, Anger = 3, Wink = 4, Cross = 5, Star = 6, };
    public enum Priority { Lowest = 1, Low = 2, Normal = 3, High = 4, Highest = 5, };
    private EyeType eyeType = EyeType.Normal;
    private EyeType newEyeType = EyeType.Normal;
    private Priority priority = Priority.Lowest;
    public string charaName;

    // Use this for initialization
    void Awake()
    {
        isTalking = false;
        windowText = this.GetComponent<Text>();
        eyeAnimator = GameObject.Find("stand_" + charaName + "_eye").GetComponent<Animator>();
        mouseAnimator = GameObject.Find("stand_" + charaName + "_mouse").GetComponent<Animator>();

        windowText.text = "";
        newText = "";
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
        mouseAnimator.SetBool("isTalking", isTalking);

        if (eyeType != newEyeType)
        {
            eyeType = newEyeType;
            eyeAnimator.SetInteger("eyeType", (int)eyeType);
        }
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
        this.newEyeType = eyeType;
        windowText.text = "";
        textIndex = 0;
    }

    public bool GetIsTalking()
    {
        return isTalking;
    }
}
