using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
   {
    public Transform playerTransform;
    private DayNightCycle dayNightCycle;
    private HealthBar healthBar;
    private FuelBar fuelBar;
    private GameManager gameManager;

    void Start()
    {
        // Mendapatkan referensi ke komponen DayNightCycle dari objek yang sama atau objek lain
        dayNightCycle = FindObjectOfType<DayNightCycle>();
        healthBar = FindObjectOfType<HealthBar>();
        fuelBar = FindObjectOfType<FuelBar>();
        gameManager = GameManager.Instance;
    }

        private void SavePosition()
    {
        
        PlayerPrefs.SetFloat("PlayerPosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerTransform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerTransform.position.z);

       
        PlayerPrefs.Save();

        Debug.Log("Player position saved!");
    }

    private void saveTime()
    {

        if (dayNightCycle != null)
        {
            dayNightCycle.SaveTime();
        }
        else
        {
            Debug.LogError("DayNightCycle tidak ditemukan!");
        }
    }

    private void saveHealth()
    {
        if (healthBar != null)
        {
            healthBar.SaveHealth();
        }
        else
        {
            Debug.LogError("Health tidak ditemukan");
        }
    }

    private void saveFuel()
    {
        if (fuelBar != null)
        {
            fuelBar.SaveFuel();
        }
        else
        {
            Debug.LogError("Fuel bar tidak ditemukan");
        }
    }
    private void saveMoney()
    {
        if (gameManager != null)
        {
            gameManager.SaveMoney(gameManager.GetPlayerMoney());
        }
        else
        {
            Debug.LogError("GameManager tidak ditemukan.");
        }
    }

    private void saveInventory()
    {
        if (gameManager != null)
        {
            gameManager.SaveInventory();
        }
        else
        {
            Debug.LogError("GameManager tidak ditemukan.");
        }
    }
    public void SaveButton()
    {
        SavePosition();
        saveTime();
        saveHealth();
        saveFuel();
        saveMoney();
        saveInventory();
    }
}


