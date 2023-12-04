using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour  
{
    [SerializeField]
    private GameObject SettingScreeen;

    public GameObject CreditScreen1;
    public GameObject CreditScreen2;

    private bool isCreditScreen1Active = true;
    public void PlayGame()
    {
        ClearPlayerPrefs();
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }

    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
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
        CreditScreen1.SetActive(true);

        if (isCreditScreen1Active)
        {
            if (CreditScreen1.activeSelf && Input.GetKeyDown(KeyCode.Space))
            {
                CreditScreen1.SetActive(false);
                CreditScreen2.SetActive(true);
                isCreditScreen1Active = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreditScreen1.SetActive(false);
                CreditScreen2.SetActive(false);

                isCreditScreen1Active = true;
                
            }
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
