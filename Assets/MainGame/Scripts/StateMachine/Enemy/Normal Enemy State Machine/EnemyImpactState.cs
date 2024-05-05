using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using UnityEngine;

namespace MainGame.StateMachine
{
    public class EnemyImpactState : EnemyBaseState
    {
        private readonly int Impact = Animator.StringToHash("Impact");

        private const float CrossFadeDuration = .1f;
        private  float duration = 0.5f;
        
        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState() 
        {
            stateMachine.Animator.CrossFadeInFixedTime(Impact, CrossFadeDuration);
            stateMachine.WeaponHandler.DisableWeapon();
        }

        public override void UpdateState(float deltaTime)
        {
            Move(deltaTime);

            if (stateMachine.PlayerInput.IsAttacking)
            {
                stateMachine.SwitchState(new EnemyBlockState(stateMachine));
                return;
            }
            
            duration -= deltaTime;
            if (duration <= 0)
            {
                stateMachine.SwitchState( new EnemyIdleState(stateMachine));
            }
        }

        public override void ExitState()
        {
        }
    }
}