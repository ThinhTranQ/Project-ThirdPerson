using System.Collections;
using System.Collections.Generic;
using MainGame.Services;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject         weaponLogic;
    [SerializeField] private PlayerStateMachine playerStateMachine;
    private                  int                index;

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
        index++;
        if (index % 2 == 0)
        {
            AudioService.instance.PlaySfx(SoundFXData.Swing2);
        }
        else
        {
            AudioService.instance.PlaySfx(SoundFXData.Swing1);
        }
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