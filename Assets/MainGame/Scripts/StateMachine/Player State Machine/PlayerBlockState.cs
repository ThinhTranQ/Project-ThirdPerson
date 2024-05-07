﻿using UnityEngine;

namespace MainGame.StateMachine
{
    public class PlayerBlockState : PlayerBaseState
    {
        private readonly int   Block             = Animator.StringToHash("Block");
        private const    float CrossFadeDuration = .1f;

       
        public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        { 
            stateMachine.Animator.CrossFadeInFixedTime(Block, CrossFadeDuration);
            stateMachine.TriggerBlock(true);
        }

        public override void UpdateState(float deltaTime)
        {
            Move(deltaTime);
            
            
            if (!stateMachine.InputReader.IsBlocking)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
               
                return;
            }
            
            if (stateMachine.Targeter.GetCurrentTarget() == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }
        }

        public override void ExitState()
        {
            stateMachine.Health.SetInvulnerable(false);
            stateMachine.TriggerBlock(false);
        }
    }
}