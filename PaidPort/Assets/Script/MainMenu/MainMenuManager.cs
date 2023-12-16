using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour  
{
    [SerializeField]
    private GameObject SettingScreeen;

    [SerializeField]
    private Slider sfxSlider;

    [SerializeField]
    private Slider MusicSlider;
    private void Awake()
    {
        sfxSlider.onValueChanged.AddListener(this.OnSfxChanged);
        MusicSlider.onValueChanged.AddListener(this.OnBgmChanged);
    }
    void Start()
    {
        Audioplayer.instance.PlayMusic(1);
    }

    public void Setting()
    {
        SettingScreeen.SetActive(true);

        if (PlayerPrefs.HasKey("bgmVol"))
        {
            MusicSlider.value = PlayerPrefs.GetFloat("bgmVol");
        }

        if (PlayerPrefs.HasKey("sfxVol"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");
        }
    }
    private void OnSfxChanged(float value)
    {
        Audioplayer.instance.ChangeSfxVolume(value);
    }
    private void OnBgmChanged(float value)
    {
        Audioplayer.instance.ChangeBgmVolume(value);
    }

    public void ExitSetting()
    {
        SettingScreeen.SetActive(false);
    }
    public void Credit()
    {

        SceneManager.LoadScene("Credit");
       
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
