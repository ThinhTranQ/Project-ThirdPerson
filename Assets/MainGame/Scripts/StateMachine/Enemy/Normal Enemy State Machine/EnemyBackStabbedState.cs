using UnityEngine;

public class EnemyBackStabbedState : EnemyBaseState
{
    private readonly int   BackStab          = Animator.StringToHash("BackStab");
    private const    float CrossFadeDuration = .1f;
    private          bool  isTrigger;
    private          bool  triggerBlood;

    private float backStabTime = 3;
    
    public EnemyBackStabbedState(EnemyStateMachine stateMachine) : base(stateMachine)
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
        // var currentTime = stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        // // if (currentTime >= 0.7f)
        // // {
        // //     if (triggerBlood) return;
        // //     triggerBlood = true;
        // //     EffectManager.Instance.SpawnBloodStabParticle(stateMachine.bloodSpawn);
        // // }
        // //
        // if (currentTime >= 0.9f)
        // {
        //    
        // }
    }

    public override void ExitState()
    {
       
    }
}