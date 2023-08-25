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

    public void EnableVulnerable()
    {
        playerStateMachine.canDeflect = true;
        playerStateMachine.Health.SetInvulnerable(true);
    }
     public void DisableVulnerable()
    {
        playerStateMachine.canDeflect = false;
    }
    
    
}
