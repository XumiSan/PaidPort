using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayButton : MonoBehaviour 
{
    [SerializeField]
    private GameObject PauseScreen;
    [SerializeField]
    private GameObject GameScreen;
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
    public void ButtonPause()
    {
        Audioplayer.instance.stopSFX();
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
        GameScreen.SetActive(false);
    }
    public void Resume()
    {
        
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        GameScreen.SetActive(true);
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
    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public void Retry()
    {
        ClearPlayerPrefs();
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1f;
    }
    public void RetrunToMainMenu()
    {
        ClearPlayerPrefs();
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
