using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using UnityEngine;

public class EnemyExhaustedState : EnemyBaseState
{
    private readonly int   Exhausted             = Animator.StringToHash("Exhausted");
    private const    float CrossFadeDuration = .1f;

    private float tiredTime = 5f;
    
    public EnemyExhaustedState(EnemyStateMachine stateMachine) : base(stateMachine)
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
           stateMachine.SwitchState(new EnemyIdleState(stateMachine));
       }
    }

    public override void ExitState()
    {
      
    }
}