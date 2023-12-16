using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayButton : MonoBehaviour 
{
    [SerializeField]
    private GameObject PauseScreen;
    [SerializeField]
    private GameObject GameScreen;
    [SerializeField]
    private GameObject SettingScreeen;
    public void ButtonPause()
    {
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
