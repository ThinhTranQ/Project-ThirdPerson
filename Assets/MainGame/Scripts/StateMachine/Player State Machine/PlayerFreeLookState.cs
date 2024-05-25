using System.Collections;
using System.Collections.Generic;
using MainGame.StateMachine;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private static readonly int FreeLookSpeed = Animator.StringToHash("FreeLookSpeed");
    private static readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    private const float ANIMATOR_DAMP_TIME = .1f;

    private const float CROSS_FADE_DURATION = .1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){}
    
    public override void EnterState()
    {
        stateMachine.InputReader.TargetEvent += OnTargetTrigger;
        stateMachine.InputReader.SkillEvent  += OnUseSkill;
        
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CROSS_FADE_DURATION);

    }

    public override void UpdateState(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        
        var movement = CalculateMovement();
        Move(movement * (stateMachine.FreeLookMovementSpeed), deltaTime);

        
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
        stateMachine.InputReader.SkillEvent  += OnUseSkill;
    }

    private void OnUseSkill()
    {
        stateMachine.OnUseSKill();
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
