using System;
using UnityEngine;

public class PlayerCollideManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            other.GetComponent<IInteractable>().DoAction();
        }
    }
}