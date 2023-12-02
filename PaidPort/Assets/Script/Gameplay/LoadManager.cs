using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
   {
    public string gameplaySceneName = "Gameplay"; // Nama scene gameplay

    private void LoadPosition()
    {
        Time.timeScale = 1f;
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            // Ambil posisi pemain dari PlayerPrefs
            float posX = PlayerPrefs.GetFloat("PlayerPosX");
            float posY = PlayerPrefs.GetFloat("PlayerPosY");
            float posZ = PlayerPrefs.GetFloat("PlayerPosZ");

            // Setel posisi pemain
            Vector3 playerPosition = new Vector3(posX, posY, posZ);

            // Memuat scene gameplay
            SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);

            // Callback untuk memastikan scene sudah dimuat
            SceneManager.sceneLoaded += (scene, loadSceneMode) =>
            {
                if (scene.name == gameplaySceneName)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        player.transform.position = playerPosition;
                        Debug.Log("Player position loaded!");
                    }
                    else
                    {
                        Debug.LogWarning("Player GameObject not found!");
                    }
                }
            };
        }
        else
        {
            Debug.LogWarning("No saved player position found!");
        }
    }

    public void LoadButton()
    {
        LoadPosition();
    }
}

