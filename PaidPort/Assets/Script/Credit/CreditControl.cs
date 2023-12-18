using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditControl : MonoBehaviour
{
    public GameObject CreditScreen1;

    private bool isCreditScreen1Active = true;
    void Start()
    {
        Audioplayer.instance.PlayMusic(3);
        CreditScreen1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCreditScreen1Active)
        {
            if (CreditScreen1.activeSelf && Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MainMenu");

                isCreditScreen1Active = true;
            }
        }
    }
}
