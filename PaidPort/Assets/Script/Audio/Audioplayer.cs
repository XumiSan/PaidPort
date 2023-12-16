using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Audioplayer : MonoBehaviour
{
    public static Audioplayer instance;

    [SerializeField]
    private AudioSource Music;
    [SerializeField]
    private AudioSource SFX;

    [SerializeField]
    private List<AudioClip> MusicTrack;
    [SerializeField]
    private List<AudioClip> SFXTrack;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;

        }


        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }


    private void Start()
    {
        GetSavedVolume();
    }


    public void PlayMusic(int index)
    {
        if (Music.clip != null)
        {
            Music.Stop();
        }
        Music.clip = MusicTrack[index];
        Music.Play();
    }


    public void PlaySFX(int index)
    {
        if (SFX.clip != null)
        {
            SFX.Stop();
        }
        SFX.clip = SFXTrack[index];
        SFX.Play();
    }

    public void StopMusic()
    {
        Music.Stop();
    }
    public void ResumeMusic()
    {
        Music.Play();
    }
    public void ChangeSfxVolume(float volume)
    {
        SFX.volume = volume;

        PlayerPrefs.SetFloat("sfxVol", volume);
    }

    public void ChangeBgmVolume(float volume)
    {
        Music.volume = volume;

        PlayerPrefs.SetFloat("bgmVol", volume);
    }

    private void GetSavedVolume()
    {
        if (PlayerPrefs.HasKey("bgmVol"))
        {
            Music.volume = PlayerPrefs.GetFloat("bgmVol");
        }

        if (!PlayerPrefs.HasKey("bgmVol"))
        {
            PlayerPrefs.SetFloat("bgmVol", Music.volume);
        }

        if (PlayerPrefs.HasKey("sfxVol"))
        {
            SFX.volume = PlayerPrefs.GetFloat("sfxVol");
        }

        if (!PlayerPrefs.HasKey("sfxVol"))
        {
            PlayerPrefs.SetFloat("sfxVol", SFX.volume);
        }
    }

    public void AudioValueSave()
    {
        float bgm = PlayerPrefs.GetFloat("bgmVol");
        float sfx = PlayerPrefs.GetFloat("sfxVol");

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("bgmVol", bgm);
        PlayerPrefs.SetFloat("sfxVol", sfx);
    }
}


