using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public string gameplaySceneName = "Gameplay"; // Nama scene gameplay
    public Button continueButton;

    private Vector3 lastPlayerPosition; // Menyimpan posisi terakhir pemain
    private bool isContinued;

    private void Start()
    {
        CheckSavedData();
    }
    private void CheckSavedData()
    {
       
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            continueButton.interactable = true;
            isContinued = true;
        }
        else
        {
            
            continueButton.interactable = false;
            isContinued = false;
            ColorBlock colors = continueButton.colors;
            colors.normalColor = Color.clear;
            colors.highlightedColor = Color.clear;
            colors.pressedColor = Color.clear;
            colors.selectedColor = Color.clear;
            colors.disabledColor = Color.clear;

            Text buttonText = continueButton.GetComponentInChildren<Text>();
            buttonText.color = Color.gray;
           


        }
    }

    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void NewGame()
    {
        ClearPlayerPrefs(); // Menghapus semua data PlayerPrefs saat tombol "New Game" ditekan
        lastPlayerPosition = Vector3.zero; // Setel posisi pemain ke posisi awal, misalnya di titik nol
        isContinued = false; // Reset status pemain telah melanjutkan permainan sebelumnya
        LoadNewGame();
        Time.timeScale = 1f;
    }

    private void LoadNewGame()
    {
        SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    private void LoadSavedGame()
    {
        float posX = PlayerPrefs.GetFloat("PlayerPosX");
        float posY = PlayerPrefs.GetFloat("PlayerPosY");
        float posZ = PlayerPrefs.GetFloat("PlayerPosZ");

        lastPlayerPosition = new Vector3(posX, posY, posZ);
        LoadGameplayScene(lastPlayerPosition);
    }

    private void LoadGameplayScene(Vector3 playerPosition)
    {
        SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == gameplaySceneName)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = lastPlayerPosition; // Atur ulang posisi pemain ke posisi terakhir
                Debug.Log("Player position loaded!");
            }
            else
            {
                Debug.LogWarning("Player GameObject not found!");
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void LoadButton()
    {
        if (!isContinued)
        {
            lastPlayerPosition = Vector3.zero; // Jika pemain belum melanjutkan permainan sebelumnya, atur ulang posisi pemain saat tombol "Load" ditekan
        }
        LoadSavedGame();
        Time.timeScale = 1f;
    }
}


