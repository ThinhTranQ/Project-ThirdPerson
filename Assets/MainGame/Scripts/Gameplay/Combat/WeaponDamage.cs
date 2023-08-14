using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider source;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private float damage;
    
    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == source) return;

        if (alreadyCollidedWith.Contains(other)) return;
        
        alreadyCollidedWith.Add(other);
        
        if (other.TryGetComponent<Health>(out var health))
        {
            health.DealDamage(damage);
        }
    }

    public void SetAttackDamage(float damage)
    {
        this.damage = damage;
    }
}
