using System.Collections;
using System.Collections.Generic;
using MainGame.StateMachine;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private static readonly int   TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private static readonly int   TargetingForwardHash   = Animator.StringToHash("TargetingForward");
    private static readonly int   TargetingRightHash     = Animator.StringToHash("TargetingRight");
    private const           float CROSS_FADE_DURATION    = .1f;

    private Vector2 dodgingDirectionInput;
    private float   remainingDodgeTime;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.InputReader.CancelEvent += OnCancelTargetingState;
        stateMachine.InputReader.DodgeEvent  += OnDodge;
      
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CROSS_FADE_DURATION);
    }


    public override void UpdateState(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            Debug.Log("attack");
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockState(stateMachine));
            return;
        }

        if (stateMachine.Targeter.GetCurrentTarget() == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        var movement = CalculateMovement();

        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        UpdateAnimation(deltaTime);

        FaceTarget();
    }


    public override void ExitState()
    {
       
        stateMachine.InputReader.DodgeEvent  -= OnDodge;
        stateMachine.InputReader.CancelEvent -= OnCancelTargetingState;
    }

 

    private void OnDodge()
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero) return;
        
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));
    }

    private void OnCancelTargetingState()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        var movement = new Vector3();
        var stateMachineTransform = stateMachine.transform;
        movement += stateMachineTransform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachineTransform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimation(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, .1f, deltaTime);
        }
        else
        {
            var value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, .1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, .1f, deltaTime);
        }
        else
        {
            var value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value, .1f, deltaTime);
        }
    }
}