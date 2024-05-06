using MainGame.StateMachine;
using UnityEngine;

public class PlayerBackStabState : PlayerBaseState
{
    private readonly int   BackStab          = Animator.StringToHash("BackStab");
    private const    float CrossFadeDuration = .1f;
    public PlayerBackStabState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Enter BackStab ");
        stateMachine.Animator.CrossFadeInFixedTime(BackStab, CrossFadeDuration);
        
    }

    public override void UpdateState(float deltaTime)
    {
        var currentTime = stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (currentTime >= 0.9f)
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

    public override void ExitState()
    {
       
    }
}