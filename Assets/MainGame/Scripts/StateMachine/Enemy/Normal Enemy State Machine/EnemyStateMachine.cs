using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Gameplay.Combat;
using MainGame.StateMachine;
using MainGame.StateMachine.Enemy.Normal_Enemy_State_Machine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator            Animator            { get; protected set; }
    [field: SerializeField] public CharacterController CharacterController { get; protected set; }
    [field: SerializeField] public ForceReceiver       ForceReceiver       { get; protected set; }
    [field: SerializeField] public NavMeshAgent        Agent               { get; protected set; }
    [field: SerializeField] public WeaponDamage        Weapon              { get; protected set; }
    [field: SerializeField] public WeaponHandler       WeaponHandler       { get; protected set; }
    [field: SerializeField] public Health              Health              { get; protected set; }
    [field: SerializeField] public Target              Target              { get; protected set; }
    [field: SerializeField] public Ragdoll             Ragdoll             { get; protected set; }

    [field: SerializeField] public EnemyCombo Combo { get; private set; }
    
    [field: SerializeField] public BlockDurability BlockDurability { get; private set; }

    [field: SerializeField] public float PlayerChasingRange { get; protected set; }
    [field: SerializeField] public float MovementSpeed      { get; protected set; }
    [field: SerializeField] public float AttackRange        { get; protected set; }
    [field: SerializeField] public bool CanInterrupt        { get; protected set; }

    public Health Player { get; private set; }

    protected virtual void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie        += HandleDie;
    }

    protected virtual void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie        -= HandleDie;
    }

    protected virtual void HandleTakeDamage()
    {
        if (!CanInterrupt) return;
        SwitchState(new EnemyImpactState(this));
    }

    protected virtual void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    protected virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

    [Button]
    public void TransitionToAttack()
    {
        SwitchState(new EnemyAttackState(this, 0));
    }

    public void ImplementNewCombo(ComboList comboList)
    {
        Animator.runtimeAnimatorController = comboList.animator;
        AttackRange                        = comboList.attackRange;

    }


    public void ChangeCombo()
    {
        Combo.ChangeCombo();
        
    }

    public void SetInterrupt(bool canInterrupt)
    {
        CanInterrupt = canInterrupt;
    }
}