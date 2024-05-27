using UnityEngine;

public class IronSkill : BaseSkill
{
    private static readonly int                Iron                = Animator.StringToHash("Buff 2");
   
    public override void ActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.ActiveSkill(playerStateMachine);
        animator.CrossFadeInFixedTime(Iron, CROSS_FADE_DURATION);
        playerStateMachine.PlayerSkillManager.isUsingSkill = true;
        player.Health.SetInvulnerableBySkill(true);
        startCastSkill = true;
        
    }

    public override void DeActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.DeActiveSkill(playerStateMachine);
       
        playerStateMachine.PlayerSkillManager.isUsingSkill = false;
        player.Health.SetInvulnerableBySkill(false);  
        
    }
}