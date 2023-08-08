using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    public void SwitchState(State newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }
    
    void Update()
    {
        // check that current state is null
        // null conditional operator and do not work with monobehavior
        currentState?.UpdateState(Time.deltaTime);
    }
}