using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using UnityEngine;

public class EnemyBlockState : EnemyBaseState
{
    private readonly int   Block             = Animator.StringToHash("Block");
    private const    float CrossFadeDuration = .1f;

    private float blockTime = 0.5f;
    
    public EnemyBlockState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void EnterState()
    {
        Debug.Log("Enter Block State");
        stateMachine.Animator.CrossFadeInFixedTime(Block, CrossFadeDuration);
        stateMachine.Health.SetInvulnerable(true);
    }

    public override void UpdateState(float deltaTime)
    {
        Move(deltaTime);

        if (stateMachine.PlayerInput.IsAttacking)
        {
            blockTime = 0.5f;
        }

        blockTime -= deltaTime;
        if (blockTime <= 0)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }

    }

    public override void ExitState()
    {
        stateMachine.Health.SetInvulnerable(false);
    }
}