using System;
using UnityEngine;

public class BaseSkill : MonoBehaviour, ISkill
{
    public bool  isDoneCasting;
    public bool  isDoneSkill;
    public bool  startCastSkill;
    public float transitionTime;

    public float skillDuration;
    public float maxSkillDuration;

    public float castingDuration;
    public float maxCastDuration;
    public virtual void ActiveSkill(PlayerStateMachine playerStateMachine)
    {
    }

    public virtual void DeActiveSkill(PlayerStateMachine playerStateMachine)
    {
    }

    public virtual void UpdateSkill()
    {
       
    }

    private void Update()
    {
        UpdateSkill();
    }

    protected virtual void ReloadSkill()
    {
        startCastSkill  = false;
        isDoneSkill     = false;
        skillDuration   = maxSkillDuration;
        castingDuration = maxCastDuration;
    }
}

public interface ISkill
{
    public void ActiveSkill(PlayerStateMachine playerStateMachine);

    public void DeActiveSkill(PlayerStateMachine playerStateMachine);
}