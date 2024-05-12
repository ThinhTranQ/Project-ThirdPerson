using System;
using MainGame.Services;
using UnityEngine;

namespace MainGame.Gameplay.Combat
{
    public class EnemyWeaponDamage : WeaponDamage
    {
        private EnemyStateMachine enemyStateMachine;
       
        private void Awake()
        {
            enemyStateMachine = GetComponentInParent<EnemyStateMachine>();
        }

        protected override void DealDamageToEnemy(Collider other)
        {
            if (other.TryGetComponent<Damagable>(out _))
            {
                var isPlayer = other.TryGetComponent<PlayerStateMachine>(out var playerStateMachine);

                if (playerStateMachine.Fainted)
                {
                    playerStateMachine.TriggerBackStabState();
                    enemyStateMachine.TriggerDoBackStab();
                    return;
                }
                
                if (other.TryGetComponent<ForceReceiver>(out var forceReceiver))
                {
                    var direction = (other.transform.position - source.transform.position).normalized;
                    forceReceiver.AddForce(direction * knockBack);
                }

                if (isPlayer)
                {
                    if (playerStateMachine.CanDeflect)
                    {
                        EffectManager.Instance.SpawnPerfectParry(other.ClosestPoint(transform.position));
                        enemyStateMachine.BlockDurability.IncreaseBlock(10, isPerfectParry: false);
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        playerStateMachine.BlockDurability.IncreaseBlock(10, isPerfectParry: false);
                    }
                    if (playerStateMachine.IsBlocking)
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

                EffectManager.Instance.SpawnHitEffect(other.ClosestPoint(transform.position));
                if (other.TryGetComponent<Health>(out var health))
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }
}