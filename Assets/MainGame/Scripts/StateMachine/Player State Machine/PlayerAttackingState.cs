using UnityEngine;

namespace MainGame.StateMachine
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private float previousFrameTIme;

        private Attack attack;

        private bool isAppliedForce;
        public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            attack = stateMachine.AttackCombo[attackIndex];
        }

        public override void EnterState()
        {
            stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        }

        public override void UpdateState(float deltaTime)
        {
            Move(deltaTime);
            
            FaceTarget();
            
            var normalizeTime = GetNormalizeTime();

            // if normalize time is equal or more than 1 so the animation is finish
            if (normalizeTime >= previousFrameTIme && normalizeTime < 1)
            {
                // apply force when time 
                if (normalizeTime >= attack.ForceTime)
                {
                    TryApplyForce();
                }
                
                if (stateMachine.InputReader.IsAttacking)
                {
                    TryComboAttack(normalizeTime);
                }
            }
            else
            {
                if (stateMachine.Targeter.GetCurrentTarget() != null)
                {
                    stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                }
                else
                { 
                    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                }
            }

            previousFrameTIme = normalizeTime;
        }


        public override void ExitState()
        {
        }

        private void TryComboAttack(float normalizeTime)
        {
           
            
            
            if (attack.ComboStateIndex == -1) return;

            if (normalizeTime < attack.ComboAttackTime) return;

            stateMachine.SwitchState
            (
                new PlayerAttackingState
                (
                    stateMachine,
                    attack.ComboStateIndex
                )
            );
        }

        private void TryApplyForce()
        {
            if (isAppliedForce) return;
            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
            isAppliedForce = true;
        }
        private float GetNormalizeTime()
        {
            var currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            var nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);
 
            if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
            {
                return nextInfo.normalizedTime;
            }
            else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
            {
                return currentInfo.normalizedTime;
            }
            else
            {
                return 0;
            }
        }
    }
}