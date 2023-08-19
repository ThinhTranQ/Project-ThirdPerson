using UnityEngine;

namespace MainGame.StateMachine
{
    public class PlayerDodgingState : PlayerBaseState
    {
        private readonly int DodgeBlendTree = Animator.StringToHash("DodgeBlendTree");
        private readonly int DodgeForward   = Animator.StringToHash("DodgeForward");
        private readonly int DodgeRight     = Animator.StringToHash("DodgeRight");

        private Vector3 dodgingDirection;
        private float   remainingDodgeTime;

        private const float CrossFadeDuration = .1f;

        public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirection) : base(stateMachine)
        {
            this.dodgingDirection = dodgingDirection;
        }

        public override void EnterState()
        {
            stateMachine.Health.SetInvulnerable(true);
            remainingDodgeTime = stateMachine.DodgeDuration;
            
            stateMachine.Animator.SetFloat(DodgeForward, dodgingDirection.y);
            stateMachine.Animator.SetFloat(DodgeRight, dodgingDirection.x);
            stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTree, CrossFadeDuration);
        }

        public override void UpdateState(float deltaTime)
        {
            var movement = new Vector3();
            var stateMachineTransform = stateMachine.transform;
            movement += stateMachineTransform.right * (dodgingDirection.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration);
            movement += stateMachineTransform.forward * (dodgingDirection.y * stateMachine.DodgeDistance / stateMachine.DodgeDuration);

            Move(movement, deltaTime);

            FaceTarget();

            remainingDodgeTime -= deltaTime;
            if (remainingDodgeTime < 0f)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            // if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown)
            //     remainingDodgeTime =  Mathf.Max(remainingDodgeTime - deltaTime, 0f);
            // {
            //     return;
            // }
        }

        public override void ExitState()
        {
            stateMachine.Health.SetInvulnerable(false);
        }
    }
}