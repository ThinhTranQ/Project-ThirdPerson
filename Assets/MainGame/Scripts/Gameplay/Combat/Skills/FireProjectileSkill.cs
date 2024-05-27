using UnityEngine;

public class FireProjectileSkill : BaseSkill
{
    private static readonly int Summon = Animator.StringToHash("Buff");
    public override void ActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.ActiveSkill(playerStateMachine);
        animator.CrossFadeInFixedTime(Summon, CROSS_FADE_DURATION);
        playerStateMachine.PlayerSkillManager.isUsingSkill = true;
        
        EffectManager.Instance.SpawnBuffParticle(transform);
        playerStateMachine.WeaponHandler.SetTriggerSkill(true);
        
        startCastSkill = true;
    }
    
    public override void DeActiveSkill(PlayerStateMachine playerStateMachine)
    {
        base.DeActiveSkill(playerStateMachine);
     
        playerStateMachine.WeaponHandler.SetTriggerSkill(false);
        playerStateMachine.PlayerSkillManager.isUsingSkill = false;
       
    }
}