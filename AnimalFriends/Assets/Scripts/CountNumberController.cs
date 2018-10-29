using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountNumberController : MonoBehaviour
{
    private Animator animator;
    bool isPlaying = false;

    int stateIdleHash, stateCountNumberHash, stateEndHash;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        stateIdleHash = Animator.StringToHash("Base Layer.Idle");
        stateCountNumberHash = Animator.StringToHash("Base Layer.CountNumber");
        stateEndHash = Animator.StringToHash("Base Layer.End");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetIsPlaying(bool isPlaying)
    {
        this.isPlaying = isPlaying;
        animator.SetBool("isPlaying", (bool)isPlaying);
    }


    public bool IsPlayingAnimation()
    {
        AnimatorStateInfo anim = animator.GetCurrentAnimatorStateInfo(0);
        int nowHash = anim.nameHash;
        return (nowHash == stateCountNumberHash);
    }

    public void PlayThreeSound()
    {
        AudioManager.Instance.PlaySE("three", 0.3f);
    }
    public void PlayTwoSound()
    {
        AudioManager.Instance.PlaySE("two", 0.3f);
    }
    public void PlayOneSound()
    {
        AudioManager.Instance.PlaySE("one", 0.3f);
    }
    public void PlayGoSound()
    {
        AudioManager.Instance.PlaySE("go", 0.3f);
    }
}
