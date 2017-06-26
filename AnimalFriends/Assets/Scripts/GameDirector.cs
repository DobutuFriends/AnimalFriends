using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    GameObject hpGauge;

    void Start()
    {
        this.hpGauge = GameObject.Find("hpGauge");
    }

    public void DecreaseHp(int decreaseHp)
    {
        this.hpGauge.GetComponent<Image>().fillAmount -= decreaseHp*0.001f;
    }

    public void IncreaseHp(int increaseHp)
    {
        this.hpGauge.GetComponent<Image>().fillAmount += increaseHp*0.001f;
    }
}