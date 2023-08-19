using UnityEngine;

namespace MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine
{
    public class EnemyAttackState : EnemyBaseState
    {
        private readonly int   Attack             = Animator.StringToHash("Attack");
        private const    float TransitionDuration = .1f;

        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            
            
            stateMachine.Weapon.SetAttackDamage(stateMachine.AttackDamage, stateMachine.AttackKnockBack);

            stateMachine.Animator.CrossFadeInFixedTime(Attack, TransitionDuration);
        }

        public override void UpdateState(float deltaTime)
        {
            if (GetNormalizeTime(stateMachine.Animator) >= 1)
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            }
            FacePlayer();
        }

        public override void ExitState()
        {
        }
    }
}