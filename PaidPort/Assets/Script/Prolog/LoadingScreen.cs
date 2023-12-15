using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Slider loadingBar;
    [SerializeField]
    private GameObject prologScreen;
    [SerializeField]
    private GameObject loadingScreen;
    public void LoadLevel(string levelName)
    {
        prologScreen.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadAsync(levelName));
    }

    IEnumerator LoadAsync(string levelName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        operation.allowSceneActivation = false;

        float loadTime = 5f; // Waktu pemuatan palsu
        float timer = 0f;
        float targetProgress = 0.9f;

        while (timer < loadTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / loadTime * targetProgress);
            loadingBar.value = progress;

            yield return null;
        }

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;


            if (operation.progress >= targetProgress)
            {
               
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}


