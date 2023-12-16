using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditControl : MonoBehaviour
{
    public GameObject CreditScreen1;
    public GameObject CreditScreen2;

    private bool isCreditScreen1Active = true;
    void Start()
    {
        Audioplayer.instance.PlayMusic(2);
        CreditScreen1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
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
                SceneManager.LoadScene("MainMenu");

                isCreditScreen1Active = true;

                

            }
        }
    }
}
