using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashControl : MonoBehaviour
{
   public AudioSource splash;
    public AudioClip clip;
    void Start()
    {
        splash.PlayOneShot(clip);
    }

    void entry()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
