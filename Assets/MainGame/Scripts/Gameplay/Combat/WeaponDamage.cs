using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider source;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private float damage;
    private float knockback;
    
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

        if (other.TryGetComponent<ForceReceiver>(out var forceReceiver))
        {
            var direction = (other.transform.position - source.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttackDamage(float damage, float knockback)
    {
        this.damage    = damage;
        this.knockback = knockback;
    }
}
