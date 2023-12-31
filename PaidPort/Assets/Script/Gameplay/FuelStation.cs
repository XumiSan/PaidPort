using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class FuelStation : MonoBehaviour
{
    [SerializeField]
    private GameObject FuelCanvas;
    [SerializeField]
    private GameObject FuelScreen;
    public FuelBar fuelBar;
    [SerializeField]
    private Text FeedbackTextFuel;

    private bool inArea;
    int itemCost = 500;



    void Start()
    {
        // Mendapatkan komponen PlayerHealth dari objek dengan skrip PlayerHealth
        fuelBar = GameObject.FindObjectOfType<FuelBar>();
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FuelCanvas.SetActive(true);
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FuelCanvas.SetActive(false);
            inArea = false;
        }
    }
    private void Update()
    {
        if (inArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                FuelScreen.SetActive(true);
                
            }
        }
    }
    public void Exit()
    {
        
        FuelScreen.SetActive(false);
        
    }

    public void Button()
    {
        if (GameManager.Instance.GetPlayerMoney() >= itemCost)
        {
            
            GameManager.Instance.SubtractMoney(itemCost);
            fuelBar.ResetHealth();
            StartCoroutine(DisplayLegacyText("Fuel bertambah"));
            Debug.Log("Fuel bertambah");
           
        }
        else
        {
            StartCoroutine(DisplayLegacyText("Uang tidak cukup"));
            Debug.Log("Uang tidak cukup");
        }
    }
    private IEnumerator DisplayLegacyText(string displayText)
    {
        FeedbackTextFuel.text = displayText; 
        FeedbackTextFuel.enabled = true;

        yield return new WaitForSeconds(1f); 

        FeedbackTextFuel.enabled = false;
    }
}

