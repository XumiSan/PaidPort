using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayButton : MonoBehaviour 
{
    public GameObject PauseScreen;
    public GameObject GameScreen;
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
}
