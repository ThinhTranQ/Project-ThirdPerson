using UnityEngine;

namespace MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine
{
    public class EnemyChasingState : EnemyBaseState
    {
        private static readonly int Locomotion = Animator.StringToHash("Locomotion");
        private static readonly int Speed      = Animator.StringToHash("Speed");

        private const float CROSS_FADE_DURATION = .1f;
        private const float AnimatorDampTime    = .1f;
        
        public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            stateMachine.Animator.CrossFadeInFixedTime(Locomotion, CROSS_FADE_DURATION);
        }

        public override void UpdateState(float deltaTime)
        {
            if (!IsInChaseRange())
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
                return;
            }
            else if (IsinAttackRange())
            {
                stateMachine.SwitchState(new EnemyAttackState(stateMachine));
                return;
            }

            MoveToPlayer(deltaTime);
            FacePlayer();
            stateMachine.Animator.SetFloat(Speed, 1, AnimatorDampTime, deltaTime);
             
        }

       


        public override void ExitState()
        {
            if (stateMachine.Agent.isOnNavMesh)
            {
                stateMachine.Agent.ResetPath();
                stateMachine.Agent.velocity = Vector3.zero; 
            }
            
        }
        private void MoveToPlayer(float deltaTime)
        {
            if (stateMachine.Agent.isOnNavMesh)
            {
                stateMachine.Agent.destination = stateMachine.Player.transform.position;
            
                Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);

            }
         
            // update agent movement = character controller movement
            stateMachine.Agent.velocity = stateMachine.CharacterController.velocity;
        }
        
        private bool IsinAttackRange()
        {
            if (stateMachine.Player.IsDead) return false;
            
            var distance = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

            return distance < stateMachine.AttackRange * stateMachine.AttackRange;
        }
    }
}