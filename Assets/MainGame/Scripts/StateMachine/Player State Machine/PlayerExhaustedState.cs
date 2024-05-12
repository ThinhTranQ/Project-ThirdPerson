using MainGame.StateMachine;
using UnityEngine;

public class PlayerExhaustedState : PlayerBaseState
{
    private readonly int   Exhausted         = Animator.StringToHash("Exhausted");
    private const    float CrossFadeDuration = .1f;

    private float tiredTime = 5f;
    public PlayerExhaustedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Enter Exhausted state");
        stateMachine.Fainted = true;
        stateMachine.Animator.CrossFadeInFixedTime(Exhausted, CrossFadeDuration);
    }

    public override void UpdateState(float deltaTime)
    {
        tiredTime -= deltaTime;
        if (tiredTime <= 0)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void ExitState()
    {
        stateMachine.Fainted = false;
    }
}