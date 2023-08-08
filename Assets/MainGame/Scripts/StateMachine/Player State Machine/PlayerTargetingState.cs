using System.Collections;
using System.Collections.Generic;
using MainGame.StateMachine;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}
  
    public override void EnterState()
    {
       stateMachine.InputReader.CancelEvent += OnCancelTargetingState;
    }

   

    public override void UpdateState(float deltaTime)
    {
        
    }
 
    public override void ExitState()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.InputReader.CancelEvent -= OnCancelTargetingState;
    }
    private void OnCancelTargetingState()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
