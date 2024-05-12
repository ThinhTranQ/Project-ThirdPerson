using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Services;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] protected Collider           source;
    private                    List<Collider>     alreadyCollidedWith = new List<Collider>();
    private                    PlayerStateMachine playerStateMachine;
   
    protected                  float              damage;
    protected                  float              knockBack;
    protected                  float              blockDamage;

    protected int index;

    private void Start()
    {
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
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
        if (other.TryGetComponent<EnemyStateMachine>(out var enemy))
        {
            if (enemy.Fainted)
            {
                enemy.TriggerBackStab();
                playerStateMachine.TriggerDoBackStabState();
                return;
            }
            
            enemy.BlockDurability.IncreaseBlock(blockDamage, isPerfectParry: false);

            if (enemy.IsBlocking)
            {
                index++;
                if (index > 3)
                {
                    index = 1;
                }
                switch (index)
                {
                    case 1:
                        AudioService.instance.PlaySfx(SoundFXData.Deflect1);
                        break; 
                    case 2:
                        AudioService.instance.PlaySfx(SoundFXData.Deflect2);
                        break; 
                    case 3:
                        AudioService.instance.PlaySfx(SoundFXData.Deflect3);
                        break;
                }
               
            }
        }
        
        if (other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(damage);
            enemy.BlockDurability.IncreaseBlock(blockDamage, isPerfectParry: false);
        }

        if (other.TryGetComponent<ForceReceiver>(out var forceReceiver))
        {
            var direction = (other.transform.position - source.transform.position).normalized;
            forceReceiver.AddForce(direction * knockBack);
        }

        if (other.TryGetComponent<Damagable>(out _))
        {
            Debug.Log("SSS " + other);
            EffectManager.Instance.SpawnHitEffect(other.ClosestPoint(transform.position));
        }
        
        
       
        
    }

    public void SetAttackDamage( float knockBack, float blockDamage)
    {
        this.damage      = damage;
        this.knockBack   = knockBack;
        this.blockDamage = blockDamage;
    }
}