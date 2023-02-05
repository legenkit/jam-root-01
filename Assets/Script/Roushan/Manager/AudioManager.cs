using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; set; }
    public enum AudioType
    {
        Start,
        orb,
        Grow,
        click,
        Explore,
        UI
    }
    public AudioClip crawl;

    public AudioSource Speaker;
    public AudioSource MSpeaker;
    public AudioSource RSpeaker;

    [Header("Audio's")]
    [SerializeField] SoundData[] Audios;

    #region Script Initialization
    private void Awake()
    {
        MakeInstance();
    }
    void MakeInstance()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }
    #endregion

    #region Public Methods
    public void PlayAudioClip(AudioType type)
    {
        foreach (SoundData Sounds in Audios)
        {
            if (Sounds.Type == type)
            {
                Speaker.PlayOneShot(Sounds.Audio[Random.Range(0, Sounds.Audio.Length)]);
            }
        }
    }
    public void PlayCrawl()
    {
        RSpeaker.PlayOneShot(crawl); ;
    }
    public void StopCrawl()
    {
        RSpeaker.Stop();
    }
    #endregion
}

[System.Serializable]
public struct SoundData
{
    public string AudioName;
    public AudioManager.AudioType Type;
    public AudioClip[] Audio;
}