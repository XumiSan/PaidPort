using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour  
{
    [SerializeField]
    private GameObject SettingScreeen;
    [SerializeField]
    private GameObject CreditScreen;
   public void PlayGame()
    {
       
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
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
        CreditScreen.SetActive(true);

        if (CreditScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreditScreen.SetActive(false);
            }
        }
        else
        {
            CreditScreen.SetActive(true);
        }
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
