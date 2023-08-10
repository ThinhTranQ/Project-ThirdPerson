using UnityEngine;

namespace MainGame.StateMachine
{
   public abstract class PlayerBaseState : State
   {
      protected PlayerStateMachine stateMachine;

      public PlayerBaseState(PlayerStateMachine stateMachine)
      {
         this.stateMachine = stateMachine;
      }

      protected void Move(Vector3 motion, float deltaTime)
      {
         stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
      }

      protected void FaceTarget()
      {
         if (stateMachine.Targeter.GetCurrentTarget() == null) return;

         var lookDirection = stateMachine.Targeter.GetCurrentTarget().transform.position - stateMachine.transform.position;
         lookDirection.y = 0f;

         stateMachine.transform.rotation = Quaternion.LookRotation(lookDirection);
      }

    
   }
}