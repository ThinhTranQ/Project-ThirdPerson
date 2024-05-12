using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using UnityEngine;

public class EnemyDoingStabState : EnemyBaseState
{
    private readonly int   BackStab          = Animator.StringToHash("DoStab");
    private const    float CrossFadeDuration = .1f;
    public EnemyDoingStabState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BackStab, CrossFadeDuration);
    }

    public override void UpdateState(float deltaTime)
    {
        var currentTime = stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (currentTime >= 0.9f)
        {
           stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void ExitState()
    {
       
    }
}