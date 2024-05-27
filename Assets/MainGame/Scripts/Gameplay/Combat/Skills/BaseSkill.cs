using System;
using UnityEngine;

public class BaseSkill : MonoBehaviour, ISkill
{
    public Sprite iconSkill;

    public bool isDoneCasting;

    public bool startCastSkill;

    private float skillDuration;
    public  float maxSkillDuration;

    private float castingDuration;
    public  float maxCastDuration;

    public float SkillCD { get; private set; }
    public float maxSkillCD;

    protected       PlayerStateMachine player;
    protected       Animator           animator;
    protected const float              CROSS_FADE_DURATION = .1f;

    public bool isUnlock;

    public virtual void ActiveSkill(PlayerStateMachine playerStateMachine)
    {
        player   = playerStateMachine;
        animator = player.Animator;
        SkillCD  = maxSkillCD;

        skillDuration   = maxSkillDuration;
        castingDuration = maxCastDuration;
    }

    public virtual void DeActiveSkill(PlayerStateMachine playerStateMachine)
    {
    }

    public void OnSkillDoneCooldown()
    {
        startCastSkill = false;
        isDoneCasting  = false;
    }

    public void UpdateSkillStatus(bool isUnlock)
    {
        this.isUnlock = isUnlock;
    }

    protected virtual void UpdateSkill()
    {
        if (!startCastSkill)
        {
            return;
        }

        SkillCD         -= Time.deltaTime;
        skillDuration   -= Time.deltaTime;
        castingDuration -= Time.deltaTime;

        if (castingDuration <= 0)
        {
            isDoneCasting = true;
        }

        if (skillDuration <= 0)
        {
            DeActiveSkill(player);
        }

        if (SkillCD <= 0)
        {
            OnSkillDoneCooldown();
        }
    }

    private void Update()
    {
        UpdateSkill();
    }
}

public interface ISkill
{
    public void ActiveSkill(PlayerStateMachine playerStateMachine);

    public void DeActiveSkill(PlayerStateMachine playerStateMachine);

    public void OnSkillDoneCooldown();
}