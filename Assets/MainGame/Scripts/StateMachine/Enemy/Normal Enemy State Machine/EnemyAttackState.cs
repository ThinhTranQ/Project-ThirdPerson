using UnityEngine;

namespace MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine
{
    public class EnemyAttackState : EnemyBaseState
    {
        private const float TransitionDuration = .1f;
        private       float previousFrameTIme;
        private       bool  isAppliedForce;
        private       bool  cantFacePlayer;

        private Attack attack;

        public EnemyAttackState(EnemyStateMachine stateMachine, int index) : base(stateMachine)
        {
            Debug.Log("Enter Attack State");
            attack = stateMachine.Combo.GetCurrentAttack(index);
        }

        public override void EnterState()
        {
            stateMachine.Weapon.SetAttackDamage(attack.Damage, attack.KnockBack);
            stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, TransitionDuration);
            stateMachine.SetInterrupt(attack.CanBeInterrupt);
            cantFacePlayer = attack.CantFacePlayer;
            
            
            
        }

        public override void UpdateState(float deltaTime)
        {
            Move(deltaTime);
            if (!cantFacePlayer)
            {
                FacePlayer();
            }

            var normalizeTime = GetNormalizeTime(stateMachine.Animator);

            if (normalizeTime >= previousFrameTIme && normalizeTime < 1)
            {
                // apply force when time 
                if (normalizeTime >= attack.ForceTime)
                {
                    TryApplyForce();
                }

                TryComboAttack(normalizeTime);
            }
            

            previousFrameTIme = normalizeTime;
        }

        public override void ExitState()
        {
        }

        private void TryComboAttack(float normalizeTime)
        {
            // if (attack.ComboStateIndex == -1) return;

            if (normalizeTime < attack.CanTransitionCombo) return;

            // Debug.Log($"Can transit combo {normalizeTime} {attack.CanTransitionCombo}");

            if (attack.ComboStateIndex == -1)
            {
                // Debug.Log($"Transit to chasing state");
                stateMachine.ChangeCombo();
                stateMachine.SwitchState(
                    new EnemyPatrolState(stateMachine));
                return;
            }

            stateMachine.SwitchState
            (
                new EnemyAttackState
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
    }
}