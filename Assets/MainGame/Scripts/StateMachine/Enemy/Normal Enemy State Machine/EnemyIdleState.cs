using UnityEngine;

namespace MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine
{
    public class EnemyIdleState : EnemyBaseState
    {
        private static readonly int Locomotion = Animator.StringToHash("Locomotion");
        private static readonly int Speed      = Animator.StringToHash("Speed");

        private const float CROSS_FADE_DURATION = .1f;
        private const float AnimatorDampTime = .1f;

        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            stateMachine.Animator.CrossFadeInFixedTime(Locomotion, CROSS_FADE_DURATION);
        }

        public override void UpdateState(float deltaTime)
        { 
            Move(deltaTime);
            
            if (IsInChaseRange())
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }
            
            stateMachine.Animator.SetFloat(Speed, 0, AnimatorDampTime, deltaTime);
        }

        public override void ExitState()
        {
        }
    }
}