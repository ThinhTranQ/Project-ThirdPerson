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

    protected       PlayerStateMachine player;
    protected       Animator           animator;
    protected const float              CROSS_FADE_DURATION = .1f;

    public virtual void ActiveSkill(PlayerStateMachine playerStateMachine)
    {
    }

    public virtual void DeActiveSkill(PlayerStateMachine playerStateMachine)
    {
    }

    protected virtual void UpdateSkill()
    {
        if (!startCastSkill)
        {
            return;
        }


        skillDuration   -= Time.deltaTime;
        castingDuration -= Time.deltaTime;

        if (castingDuration <= 0)
        {
            isDoneCasting = true;
        }

        if (skillDuration <= 0)
        {
            isDoneSkill = true;
            DeActiveSkill(player);
        }
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