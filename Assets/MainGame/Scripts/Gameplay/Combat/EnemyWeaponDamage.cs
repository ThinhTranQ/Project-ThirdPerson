using System;
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
            print(other);
            if (other.TryGetComponent<Damagable>(out _))
            {
                if (other.TryGetComponent<ForceReceiver>(out var forceReceiver))
                {
                    var direction = (other.transform.position - source.transform.position).normalized;
                    forceReceiver.AddForce(direction * knockback);
                }
                
                if (other.TryGetComponent<PlayerStateMachine>(out var playerStateMachine))
                {
                    if (playerStateMachine.CanDeflect)
                    {
                        print("deflect");
                        sourceHealth.DealDamage(0);
                        EffectManager.Instance.SpawnPerfectParry(other.ClosestPoint(transform.position));
                        enemyStateMachine.ChangeCombo();
                        playerStateMachine.BlockDurability.IncreaseBlock(10, isPerfectParry:true);
                        enemyStateMachine.BlockDurability.IncreaseBlock(10, isPerfectParry:false);
                        gameObject.SetActive(false);
                        return;
                    }
                    playerStateMachine.BlockDurability.IncreaseBlock(10, isPerfectParry: false);
                }
                
                EffectManager.Instance.SpawnHitEffect(other.ClosestPoint(transform.position));
                if (other.TryGetComponent<Health>(out var health))
                {
                    health.DealDamage(damage);
                }

                
            }
        }
    }
}