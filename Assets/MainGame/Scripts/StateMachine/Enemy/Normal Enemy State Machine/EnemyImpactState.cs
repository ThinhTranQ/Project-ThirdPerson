using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using UnityEngine;

namespace MainGame.StateMachine
{
    public class EnemyImpactState : EnemyBaseState
    {
        private static readonly int Impact = Animator.StringToHash("Impact");

        private const float CrossFadeDuration = .1f;
        private  float duration = 1f;
        
        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState() 
        {
            stateMachine.Animator.CrossFadeInFixedTime(Impact, CrossFadeDuration);
        }

        public override void UpdateState(float deltaTime)
        {
            Move(deltaTime);
            
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