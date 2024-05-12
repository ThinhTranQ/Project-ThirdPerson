using MainGame.StateMachine;
using UnityEngine;

public class PlayerGetStabbedState : PlayerBaseState
{
    private readonly int   BackStab          = Animator.StringToHash("BackStab");
    private const    float CrossFadeDuration = .1f;
    private          bool  isTrigger;
    private          bool  triggerBlood;
    private          float backStabTime = 3;
    public PlayerGetStabbedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Enter Back Stab State");
        stateMachine.Fainted = false;
        stateMachine.Animator.CrossFadeInFixedTime(BackStab, CrossFadeDuration);
    }

    public override void UpdateState(float deltaTime)
    {
        backStabTime -= deltaTime;
        if (backStabTime <= 0)
        {
            if (isTrigger) return;
            isTrigger = true;
            stateMachine.TriggerDeadState();
        }
    }

    public override void ExitState()
    {
        // throw new System.NotImplementedException();
    }
}