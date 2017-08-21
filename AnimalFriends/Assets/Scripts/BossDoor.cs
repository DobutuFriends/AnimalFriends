using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour {

    BoxCollider2D bc;
    public bool EnterFlagLeft;
    public bool EnterFlagRight;
    SpriteRenderer[] sr;
    [SerializeField]
    int second;

	void Start () {
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        sr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in sr)
        {
            s.enabled = false;
        }
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (EnterFlagLeft == true && EnterFlagRight == true)
            {
                StartCoroutine(FuncDisplay());
                bc.enabled = true;
            }
        }
    }

    /// <summary>
    /// 指定時間間隔でブロックを表示する
    /// </summary>
    IEnumerator FuncDisplay()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].enabled = true;
            yield return new WaitForSeconds(second);
        }
    }
}
