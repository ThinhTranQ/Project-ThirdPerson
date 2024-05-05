using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] protected Collider       source;
    private                    List<Collider> alreadyCollidedWith = new List<Collider>();
    protected                  Health         sourceHealth;
    protected                  float          damage;
    protected                  float          knockback;

    private void Start()
    {
        sourceHealth = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other == source) return;


        if (alreadyCollidedWith.Contains(other)) return;

        alreadyCollidedWith.Add(other);

        DealDamageToEnemy(other);
    }

    protected virtual void DealDamageToEnemy(Collider other)
    {
        if (other.TryGetComponent<Health>(out var health))
        {
            health.DealDamage(damage);
        }

        if (other.TryGetComponent<ForceReceiver>(out var forceReceiver))
        {
            var direction = (other.transform.position - source.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }

        if (other.TryGetComponent<Damagable>(out _))
        {
            Debug.Log("SSS " + other);
            EffectManager.Instance.SpawnHitEffect(other.ClosestPoint(transform.position));
        }

        if (other.TryGetComponent<EnemyStateMachine>(out var enemy))
        {
            enemy.BlockDurability.IncreaseBlock(20, isPerfectParry: false);
        }
        
    }

    public void SetAttackDamage(float damage, float knockback)
    {
        this.damage    = damage;
        this.knockback = knockback;
    }
}