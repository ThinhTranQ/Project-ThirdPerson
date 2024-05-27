using System;
using MainGame.StateMachine;
using UnityEngine;

public class HealingSkill : BaseSkill
{
    private static readonly int                HEAL                = Animator.StringToHash("Heal");
    public override void ActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.ActiveSkill(playerStateMachine);
        
        animator.CrossFadeInFixedTime(HEAL, CROSS_FADE_DURATION);
        playerStateMachine.PlayerSkillManager.isUsingSkill = true;
        player.BlockDurability.currentBlock                = 0;
     
        startCastSkill = true;
    }
    

    public override void DeActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.DeActiveSkill(playerStateMachine);
       
        playerStateMachine.PlayerSkillManager.isUsingSkill = false;

    }
}