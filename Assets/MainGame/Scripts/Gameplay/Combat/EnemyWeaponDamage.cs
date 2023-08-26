using UnityEngine;

namespace MainGame.Gameplay.Combat
{
    public class EnemyWeaponDamage : WeaponDamage
    {
        protected override void DealDamageToEnemy(Collider other)
        {
            if (other.TryGetComponent<PlayerStateMachine>(out var playerStateMachine))
            {
                if (playerStateMachine.canDeflect)
                {
                    print("deflect");
                    sourceHealth.DealDamage(0);
                    gameObject.SetActive(false);
                    return;
                }
            }
            base.DealDamageToEnemy(other);
        }
    }
}