using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using UnityEngine;

public class EnemyReviveState : EnemyBaseState
{
    private readonly int   Revive            = Animator.StringToHash("Revive");
    private const    float CrossFadeDuration = .1f;

    private float reviveTime = 3f;
    
    public EnemyReviveState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.Fainted = false;
        stateMachine.Animator.CrossFadeInFixedTime(Revive, CrossFadeDuration);
        stateMachine.phase2Particle.gameObject.SetActive(true);
        EffectManager.Instance.SpawnBuffParticle(stateMachine.transform);
    }

    public override void UpdateState(float deltaTime)
    {
        reviveTime -= Time.deltaTime;
        if (reviveTime <= 0)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }

    }

    public override void ExitState()
    {
       
    }
}