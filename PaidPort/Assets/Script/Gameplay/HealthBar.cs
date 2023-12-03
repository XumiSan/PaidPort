using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour

{
    public float maxHealth = 10;
    public float currentHealth;

    public Image healthBar;

    private string healthSaveKey = "PlayerHealth";

    void Start()
    {
        LoadHealth();
        UpdateHealthBar();
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SaveHealth();
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.GameOver();
        Debug.Log("Player mati");

    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        SaveHealth();
        UpdateHealthBar();
        Debug.Log("Health direset ke nilai awal: " + currentHealth);

    }
    public void SaveHealth()
    {
        PlayerPrefs.SetFloat(healthSaveKey, currentHealth);
        PlayerPrefs.Save(); 
    }

    public void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }
    public void LoadHealth()
    {
        if (PlayerPrefs.HasKey(healthSaveKey))
        {
            currentHealth = PlayerPrefs.GetFloat(healthSaveKey);
        }
        else
        {
            currentHealth = maxHealth;
        }

    }
}


