using UnityEngine;

public abstract class State
{
    public abstract void EnterState();

    public abstract void UpdateState(float deltaTime);

    public abstract void ExitState();
    
    protected float GetNormalizeTime(Animator animator)
    {
        var currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        var nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }

    
}
