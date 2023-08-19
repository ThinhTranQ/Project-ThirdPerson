using UnityEngine;

namespace MainGame.StateMachine
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override void EnterState()
        {
            stateMachine.Ragdoll.ToggleRagDoll(true);
            stateMachine.Weapon.gameObject.SetActive(false);
            GameObject.Destroy(stateMachine.Target);
        }

        public override void UpdateState(float deltaTime)
        {
        }

        public override void ExitState()
        { 
        }
    }
}