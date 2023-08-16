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

      // move with not with input but force
      protected void Move(float deltaTime)
      {
         Move(Vector3.zero, deltaTime);
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

      protected void ReturnToLocomotion()
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

    
   }
}