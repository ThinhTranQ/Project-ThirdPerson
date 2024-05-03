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
        print("Turn on Deflect");
        playerStateMachine.CanDeflect = true;
        playerStateMachine.Health.SetInvulnerable(true);
    }
     public void DisableVulnerable()
    {
        playerStateMachine.CanDeflect = false;
        
    }
    
    
}
