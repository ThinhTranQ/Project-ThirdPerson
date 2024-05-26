using UnityEngine;

public class IronSkill : BaseSkill
{
    private static readonly int                Iron                = Animator.StringToHash("Heal");
   
    public override void ActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.ActiveSkill(playerStateMachine);
        player   = playerStateMachine;
        animator = player.Animator;
        animator.CrossFadeInFixedTime(Iron, CROSS_FADE_DURATION);
        playerStateMachine.PlayerSkillManager.isUsingSkill = true;
        player.Health.SetInvulnerableBySkill(true);       
        ReloadSkill();
        startCastSkill = true;
        
    }

    public override void DeActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.DeActiveSkill(playerStateMachine);
        startCastSkill                                     = false;
        isDoneCasting                                      = false;
        isDoneSkill                                        = false;
        playerStateMachine.PlayerSkillManager.isUsingSkill = false;
        player.Health.SetInvulnerableBySkill(false);  
        
    }
}