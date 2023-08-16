using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;

    public event Action OnTakeDamage; 
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {
        if (currentHealth <= 0)
        {
            return;
        }
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        
        OnTakeDamage?.Invoke();
        
        Debug.Log(currentHealth);
    }
}