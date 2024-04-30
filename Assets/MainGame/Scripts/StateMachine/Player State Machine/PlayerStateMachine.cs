using System;
using MainGame.Gameplay.Combat;
using MainGame.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader         InputReader         { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator            Animator            { get; private set; }
    [field: SerializeField] public Targeter            Targeter            { get; private set; }
    [field: SerializeField] public Attack[]            AttackCombo         { get; private set; }
    [field: SerializeField] public ForceReceiver       ForceReceiver       { get; private set; }
    [field: SerializeField] public WeaponDamage        WeaponDamage        { get; private set; }
    [field: SerializeField] public Health              Health              { get; private set; }
    [field: SerializeField] public Ragdoll             Ragdoll             { get; private set; }

    [field: SerializeField] public float FreeLookMovementSpeed  { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping        { get; private set; }
    [field: SerializeField] public float DodgeDuration          { get; private set; }
    [field: SerializeField] public float DodgeDistance          { get; private set; }
    [field: SerializeField] public bool canDeflect          { get; set; }
    
    public Transform MainCamera        { get; private set; } 

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie        += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie        -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    private void Start()
    {
        if (Camera.main != null) MainCamera = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}