using MainGame.StateMachine;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    private BaseSkill currentSkill;
    
    public PlayerSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        currentSkill = stateMachine.PlayerSkillManager.GetCurrentSkill();
        if (currentSkill == null)
        {
            Debug.LogError("Invalid Skill Index");
        }
        currentSkill.ActiveSkill(stateMachine);
    }

    public override void UpdateState(float deltaTime)
    {
        if (currentSkill.isDoneCasting)
        {
            Debug.Log("Done casting");
            if (stateMachine.Targeter.GetCurrentTarget() != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
    }

    public override void ExitState()
    {
       
    }
}