using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour  
{
    [SerializeField]
    private GameObject SettingScreeen;

    void Start()
    {
        Audioplayer.instance.PlayMusic(1);
    }

    public void Setting()
    {
        SettingScreeen.SetActive(true);
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
