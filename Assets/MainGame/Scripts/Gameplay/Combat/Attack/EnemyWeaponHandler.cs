using System;

public class EnemyWeaponHandler : WeaponHandler
{
     private EnemyStateMachine enemyStateMachine;

     private void Start()
     {
          enemyStateMachine = GetComponentInParent<EnemyStateMachine>();
     }

     public override void EnableWeapon()
     {
          base.EnableWeapon();
          if (enemyStateMachine.canFireProjectile)
          {
               EffectManager.Instance.SpawnSlashEff(transform);
          }
     }

     public override void EnableVulnerable()
     {
          
     }

     public override void DisableVulnerable()
     {
          
     }
}