using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour

{
    public int totalFuel = 100;
    public int currentFuel;

    public float damageInterval = 2f; 
    private float nextDamageTime;

    public Image fuelBarImage;

    private string fuelSaveKey = "PlayerFuel";

    void Start()
    {
        LoadFuel();
        UpdateFuelBar();
    }

    void Update()
    {
       
        if (Time.time >= nextDamageTime)
        {
            TakeDamage(5);
            nextDamageTime = Time.time + damageInterval;
        }
    }

    void TakeDamage(int damage)
    {
        currentFuel -= damage;
        UpdateFuelBar();

       
        if (currentFuel <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.GameOver();
        Debug.Log("Player Mati");
       
    }

    public void UpdateFuelBar()
    {
        float fillAmount = (float)currentFuel / totalFuel;
        fuelBarImage.fillAmount = fillAmount;
    }
    public void SaveFuel()
    {
        PlayerPrefs.SetInt(fuelSaveKey, currentFuel);
        PlayerPrefs.Save();
    }
    public void ResetFuel()
    {
        currentFuel = totalFuel;
        UpdateFuelBar();
        Debug.Log("Health direset ke nilai awal: " + currentFuel);
        
    }
    public void LoadFuel()
    {
        if (PlayerPrefs.HasKey(fuelSaveKey))
        {
            currentFuel = PlayerPrefs.GetInt(fuelSaveKey);
        }
        else
        {
            currentFuel = totalFuel;
        }

    }

}


