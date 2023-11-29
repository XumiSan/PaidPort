using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GroundHealth : MonoBehaviour
{
    public float maxHealth = 20f;
    private float healDelay = 1.1f;
    private float currentHealth;
    private float lastDamageTime;

    private void Start()
    {
        currentHealth = maxHealth;
        lastDamageTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastDamageTime >= healDelay)
        {
            
            currentHealth = Mathf.Min(currentHealth + (Time.deltaTime / healDelay) * maxHealth, maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        lastDamageTime = Time.time;

       
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

