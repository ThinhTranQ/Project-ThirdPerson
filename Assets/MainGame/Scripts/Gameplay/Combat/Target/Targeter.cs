using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
   [SerializeField] private List<Target> targets = new List<Target>();
   [SerializeField] private Target       currentTarget;
   
   private void OnTriggerEnter(Collider other)
   {
      if (!other.TryGetComponent<Target>(out var target)) return;
      targets.Add(target);
   }

   private void OnTriggerExit(Collider other)
   {
      if (!other.TryGetComponent<Target>(out var target)) return;
      targets.Remove(target);
   }

   public bool SelectTarget()
   {
      if (targets.Count == 0) return false;

      currentTarget = targets[0];
      return true;
   }

   public void CancelTarget()
   {
      currentTarget = null;
   }
}
