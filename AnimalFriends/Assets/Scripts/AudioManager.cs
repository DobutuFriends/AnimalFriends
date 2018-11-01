using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public List<AudioClip> BGMList;
    public List<AudioClip> SEList;

    // 同時に再生可能なSE数の上限
    private readonly int SE_COUNT_LIMIT = 100;

    private AudioSource bgmSource = null;
    private List<AudioSource> seSources = null;
    private Dictionary<string, AudioClip> bgmDict = null;
    private Dictionary<string, AudioClip> seDict = null;

    private bool isFadeOut = false;

    public void Start()
    {
        // Audio Listener がなければ作る
        if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
        {
            this.gameObject.AddComponent<AudioListener>(); //DEBUG
        }
        // Audio Source を作る
        this.bgmSource = this.gameObject.AddComponent<AudioSource>();
        this.seSources = new List<AudioSource>();

        // BGM/SEリストの初期化
        this.bgmDict = new Dictionary<string, AudioClip>();
        this.seDict = new Dictionary<string, AudioClip>();

        // Inspectorから設定したAudioClipをリストに格納する
        Action<Dictionary<string, AudioClip>, AudioClip> addClipDict = (dict, c) =>
        {
            if (!dict.ContainsKey(c.name))
            {
                dict.Add(c.name, c);
            }
        };
        this.BGMList.ForEach(bgm => addClipDict(this.bgmDict, bgm));
        this.SEList.ForEach(se => addClipDict(this.seDict, se));
    }

    public void Update()
    {
        if (isFadeOut)
        {
            this.bgmSource.volume = this.bgmSource.volume * 0.98f;
            if (this.bgmSource.volume < 0.01f)
            {
                this.StopBGM();
            }
        }
    }

    public void PlaySE(string seName, float volume = 1.0f, bool loop = false)
    {
        if (!this.seDict.ContainsKey(seName))
            throw new ArgumentException(seName + " not found", "seName");

        AudioSource source = this.seSources.FirstOrDefault(s => !s.isPlaying);
        if (source == null)
        {
            if (this.seSources.Count >= this.SE_COUNT_LIMIT)
            {
                Debug.Log("SE AudioSource is full");
                return;
            }

            source = this.gameObject.AddComponent<AudioSource>();
            this.seSources.Add(source);
        }

        source.clip = this.seDict[seName];
        source.volume = volume;
        source.pitch = 1.0f;
        source.loop = loop;
        source.Play();
    }

    public void StopSE()
    {
        this.seSources.ForEach(s => s.Stop());
    }

    public void StopSE(string seName)
    {
        foreach (var source in this.seSources)
        {
            if (source.clip.name == seName)
            {
                source.Stop();
            }
        }
    }

    public void PlaySE(string seName)
    {
        PlaySE(seName, 0.3f, false);
    }

    public void PlayBGM(string bgmName, float volume = 1.0f, bool loop = true)
    {
        if (!this.bgmDict.ContainsKey(bgmName))
            throw new ArgumentException(bgmName + " not found", "bgmName");
        if (this.bgmSource.clip == this.bgmDict[bgmName])
            return;
        this.bgmSource.Stop();
        this.bgmSource.clip = this.bgmDict[bgmName];
        this.bgmSource.loop = loop;
        this.bgmSource.volume = volume;
        this.bgmSource.pitch = 1;
        this.bgmSource.Play();
        isFadeOut = false;
    }

    public void StopBGM()
    {
        this.bgmSource.Stop();
        this.bgmSource.clip = null;
        isFadeOut = false;
    }

    public void FadeOutBGM()
    {
        isFadeOut = true;
    }

    public void SetBGMPitch(float pitch)
    {
        this.bgmSource.pitch = pitch;
    }

    public void SetSEPitch(string seName, float pitch)
    {
        foreach (var source in this.seSources)
        {
            if (source.clip.name == seName)
            {
                source.pitch = pitch;
            }
        }
    }
}