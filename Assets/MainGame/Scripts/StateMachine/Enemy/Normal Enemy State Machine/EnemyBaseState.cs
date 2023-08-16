using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void FacePlayer()
    {
        if (stateMachine.Player == null) return;

        var lookDirection = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookDirection);
    }
    protected bool IsInChaseRange()
    {
       var distance = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

       return distance <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }
}
