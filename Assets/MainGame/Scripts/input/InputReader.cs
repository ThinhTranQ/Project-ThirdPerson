using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2      MovementValue { get; private set; }
    public bool         IsAttacking   { get; private set; }
    public bool         IsBlocking    { get; private set; }
    public event Action JumpEvent;

    public event Action SkillChangeEvent;
    public event Action SkillEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;


    private Controls controls;

    void Start()
    {
        // same like player inputs
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        CancelEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsBlocking = true;
        }
        else if (context.canceled)
        {
            IsBlocking = false;
        }
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        SkillEvent?.Invoke();
    }

    public void OnChangeSkill(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        SkillChangeEvent?.Invoke();
    }
}