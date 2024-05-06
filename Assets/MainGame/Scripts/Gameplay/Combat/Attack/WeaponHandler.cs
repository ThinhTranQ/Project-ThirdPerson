using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject         weaponLogic;
    [SerializeField] private PlayerStateMachine playerStateMachine;

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }

    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }

    public virtual void EnableVulnerable()
    {
        if (playerStateMachine == null) return;
        print("Turn on Deflect");
        playerStateMachine.CanDeflect = true;
        playerStateMachine.Health.SetInvulnerable(true);
    }

    public virtual void DisableVulnerable()
    {
        if (playerStateMachine == null) return;
        playerStateMachine.CanDeflect = false;
    }

    public void TriggerBlood()
    {
        EffectManager.Instance.SpawnBloodStabParticle(weaponLogic.transform);
    }

    public void TriggerBlood2()
    {
        EffectManager.Instance.SpawnBloodStabParticle(weaponLogic.transform);
    }
}