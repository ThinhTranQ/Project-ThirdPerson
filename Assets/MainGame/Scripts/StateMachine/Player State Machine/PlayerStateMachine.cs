using System;
using MainGame.Gameplay.Combat;
using MainGame.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader         InputReader            { get; private set; }
    [field: SerializeField] public CharacterController CharacterController    { get; private set; }
    [field: SerializeField] public Animator            Animator               { get; private set; }
    [field: SerializeField] public Targeter            Targeter               { get; private set; }
    [field: SerializeField] public Attack[]            AttackCombo            { get; private set; }
    [field: SerializeField] public Collider            Collider               { get; private set; }
    [field: SerializeField] public WeaponHandler       WeaponHandler          { get; private set; }
    [field: SerializeField] public ForceReceiver       ForceReceiver          { get; private set; }
    [field: SerializeField] public WeaponDamage        WeaponDamage           { get; private set; }
    [field: SerializeField] public Health              Health                 { get; private set; }
    [field: SerializeField] public BlockDurability     BlockDurability        { get; private set; }
    [field: SerializeField] public PlayerSkillManager  PlayerSkillManager     { get; private set; }
    [field: SerializeField] public float               FreeLookMovementSpeed  { get; private set; }
    [field: SerializeField] public float               TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float               RotationDamping        { get; private set; }
    [field: SerializeField] public float               DodgeDuration          { get; private set; }
    [field: SerializeField] public float               DodgeDistance          { get; private set; }
    [field: SerializeField] public bool                CanDeflect             { get; set; }
    [field: SerializeField] public bool                IsBlocking             { get; set; }

    public bool      isUsingSkill;
    public bool      Fainted;
    public Transform MainCamera { get; private set; }

    private void OnEnable()
    {
        Health.OnTakeDamage          += HandleTakeDamage;
        Health.OnDie                 += HandleDie;
        BlockDurability.OutOfStamina += HandleOutOfStamina;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage          -= HandleTakeDamage;
        Health.OnDie                 -= HandleDie;
        BlockDurability.OutOfStamina -= HandleOutOfStamina;
    }

    private void HandleOutOfStamina()
    {
        SwitchState(new PlayerExhaustedState(this));
    }

    private void HandleTakeDamage()
    {
        if (Fainted) return;
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        print("On Die");
        UIManager.Instance.ShowGameLose();
        SwitchState(new PlayerDeadState(this));
    }

    private void Start()
    {
        if (Camera.main != null) MainCamera = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

    public void TriggerDoBackStabState()
    {
        SwitchState(new PlayerDoBackStabState(this));
    }

    public void TriggerBackStabState()
    {
        SwitchState(new PlayerGetStabbedState(this));
    }

    public void TriggerBlock(bool isOn)
    {
        IsBlocking = isOn;
    }

    public void TriggerDeadState()
    {
        OnDeathExecution();
    }

    private void OnDeathExecution()
    {
        CharacterController.enabled = false;
        Health.DieByBackStab();
    }

    public void OnUseSKill()
    {
        if (!PlayerSkillManager.IsAnySkillAvailable())
        {
            print("No Skill Available");
            return;
        }

        if (PlayerSkillManager.isUsingSkill)
        {
            print("Using skill");
            return;
        }
        
        SwitchState(new PlayerSkillState(this));
        print("Use Skill");
    }
}