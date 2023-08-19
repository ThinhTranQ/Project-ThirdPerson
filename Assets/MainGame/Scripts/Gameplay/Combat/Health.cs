using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;

    private bool isInvulnerable;

    public bool IsDead => currentHealth <= 0;

    public event Action OnTakeDamage; 
    public event Action OnDie; 
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }
    public void DealDamage(float damage)
    {
        if (currentHealth <= 0)
        {
            return;
        } 

        if (isInvulnerable) return;
        
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        
        OnTakeDamage?.Invoke();

        if (currentHealth <= 0)
        {
            OnDie?.Invoke();
        }
        
        Debug.Log(currentHealth);
    }
}