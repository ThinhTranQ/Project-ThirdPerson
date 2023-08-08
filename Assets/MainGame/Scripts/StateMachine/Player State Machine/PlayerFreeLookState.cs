using System.Collections;
using System.Collections.Generic;
using MainGame.StateMachine;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private static readonly int FreeLookSpeed = Animator.StringToHash("FreeLookSpeed");

    private const float ANIMATOR_DAMP_TIME = .1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){}
    
    // we can make another variable and it would call base store the statemachine and then it would store x variable
    // private int x;
    // public PlayerTestState(PlayerStateMachine stateMachine, int x) : base(stateMachine)
    // {
    //     this.x = x;
    // }

    
    
    public override void EnterState()
    {
        stateMachine.InputReader.TargetEvent += OnTargetTrigger;

    }

    public override void UpdateState(float deltaTime)
    {
        var movement = CalculateMovement();
        stateMachine.CharacterController.Move(movement * (deltaTime * stateMachine.FreeLookMovementSpeed));

        
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeed, 0, ANIMATOR_DAMP_TIME, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(FreeLookSpeed, 1, ANIMATOR_DAMP_TIME, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

   

    public override void ExitState() 
    {
        stateMachine.InputReader.TargetEvent -= OnTargetTrigger;
    }

    private void OnTargetTrigger()
    {
        if (!stateMachine.Targeter.SelectTarget()) return;
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        var forward = stateMachine.MainCamera.forward;
        var right = stateMachine.MainCamera.right;

        forward.y = 0f;
        right.y   = 0f;
        
        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }
    
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }
   

    
}
