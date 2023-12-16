using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour

{
    [SerializeField]
    private Text displayText;
    [SerializeField]
    private Image displayImage;
    public string[] sentences;
    public Sprite[] images;
    [SerializeField]
    private float typingSpeed = 0.05f;

    [SerializeField]
    private LoadingScreen loadingScreen;

    private int index;
    private bool isTyping;

    void Start()
    {

        Audioplayer.instance.PlayMusic(2);
        StartCoroutine(AnimateImageAndText());
    }

    IEnumerator AnimateImageAndText()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            yield return StartCoroutine(DisplayImage(i));
            yield return StartCoroutine(TypeText(sentences[i]));
            isTyping = false;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            displayText.text = "";
            displayImage.enabled = false;
        }

        Debug.Log("Semua teks dan gambar telah ditampilkan.");
        LoadNextScene();
    }

    IEnumerator DisplayImage(int index)
    {
        displayImage.enabled = true;
        if (images.Length > index && images[index] != null)
        {
            displayImage.sprite = images[index];
        }
        yield return null;
    }

    IEnumerator TypeText(string sentence)
    {
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
    void LoadNextScene()
    {
        if (loadingScreen != null)
        {
            loadingScreen.LoadLevel("Gameplay");
        }
        else
        {
            Debug.LogWarning("Objek LoadingScreen belum ditetapkan di Inspector!");
        }
    }

    void Update()
    {
        if (!isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            displayText.text = "";
            displayImage.enabled = false;
        }
    }
}
