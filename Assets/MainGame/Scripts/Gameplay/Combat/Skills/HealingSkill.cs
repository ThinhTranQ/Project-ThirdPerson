using System;
using MainGame.StateMachine;
using UnityEngine;

public class HealingSkill : BaseSkill
{
    private static readonly int                HEAL                = Animator.StringToHash("Heal");
    public override void ActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.ActiveSkill(playerStateMachine);

        player   = playerStateMachine;
        animator = player.Animator;
        animator.CrossFadeInFixedTime(HEAL, CROSS_FADE_DURATION);
        playerStateMachine.PlayerSkillManager.isUsingSkill = true;
        player.BlockDurability.currentBlock                = 0;
        ReloadSkill();
        startCastSkill = true;
    }

    // public override void UpdateSkill()
    // {
    //     if (!startCastSkill)
    //     {
    //         return;
    //     }
    //   
    //
    //     skillDuration   -= Time.deltaTime;
    //     castingDuration -= Time.deltaTime;
    //
    //     if (castingDuration <= 0)
    //     {
    //         isDoneCasting = true;
    //     }
    //     
    //     if (skillDuration <= 0)
    //     {
    //         isDoneSkill = true;
    //         DeActiveSkill(player);
    //     }
    // }

    public override void DeActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.DeActiveSkill(playerStateMachine);
        startCastSkill                                     = false;
        isDoneCasting                                      = false;
        isDoneSkill                                        = false;
        playerStateMachine.PlayerSkillManager.isUsingSkill = false;

    }
}