using UnityEngine;

namespace MainGame.StateMachine
{
    public class PlayerDeadState : PlayerBaseState
    {
        public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            // stateMachine.Ragdoll.ToggleRagDoll(true);
            stateMachine.WeaponDamage.gameObject.SetActive(false);
            
        }

        public override void UpdateState(float deltaTime)
        {
        }

        public override void ExitState()
        {
        }
    }
}