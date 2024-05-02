using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private                 float   patrolTime;
    private static readonly int     Locomotion          = Animator.StringToHash("Locomotion");
    private static readonly int     Speed               = Animator.StringToHash("Speed");
    private const           float   CROSS_FADE_DURATION = .1f;
    private const           float   AnimatorDampTime    = .1f;
    private                 Vector3 randomDirection;
    private                 bool    startPatrol;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        patrolTime = Random.Range(0, 1f);
        stateMachine.Animator.CrossFadeInFixedTime(Locomotion, CROSS_FADE_DURATION);
        GenerateRandomDirection();
        startPatrol = true;
    }

    public override void UpdateState(float deltaTime)
    {
        if (!startPatrol) return;
        FacePlayer();
        stateMachine.Animator.SetFloat(Speed, 0.3f, AnimatorDampTime, deltaTime);

        patrolTime -= Time.deltaTime;
        if (patrolTime >= 0)
        {
            Debug.Log(patrolTime);
            Move(randomDirection, deltaTime);
        }
        else
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void ExitState()
    {
       
    }

    void GenerateRandomDirection()
    {
        var randomAngle = Random.Range(0f, 360f);

        randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;
    }
}