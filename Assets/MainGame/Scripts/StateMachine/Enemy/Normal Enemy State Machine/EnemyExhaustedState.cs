using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using UnityEngine;

public class EnemyExhaustedState : EnemyBaseState
{
    private readonly int   Tired             = Animator.StringToHash("Block");
    private const    float CrossFadeDuration = .1f;

    private float tiredTime = 2f;
    
    public EnemyExhaustedState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        // stateMachine.Animator.CrossFadeInFixedTime(Tired, CrossFadeDuration);
    }

    public override void UpdateState(float deltaTime)
    {
       Debug.Log("Tired");
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