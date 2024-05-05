using UnityEngine;

namespace MainGame.StateMachine
{
    public class PlayerImpactState : PlayerBaseState
    {
        private readonly int Impact = Animator.StringToHash("Impact");

        private const float CrossFadeDuration = .1f;

        private float duration = 0.5f;
        
        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Enter impact");
            stateMachine.Animator.CrossFadeInFixedTime(Impact, CrossFadeDuration);
            stateMachine.WeaponHandler.DisableWeapon();
        }

        public override void UpdateState(float deltaTime)
        {
            Move(deltaTime);

            duration -= deltaTime;

            if (duration <= 0)
            {
                ReturnToLocomotion();
            }
        }

        public override void ExitState()
        {
        }
    }
}