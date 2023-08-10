using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
   [SerializeField] private List<Target> targets = new List<Target>();
   [SerializeField] private Target       currentTarget;
   [SerializeField] private CinemachineTargetGroup       cinemachineTargetGroup;

   private Camera mainCamera;

   private void Start()
   {
      mainCamera = Camera.main;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (!other.TryGetComponent<Target>(out var target)) return;
      targets.Add(target);
      target.OnDestroyed += RemoveTarget;
   }
   
   private void OnTriggerExit(Collider other)
   {
      if (!other.TryGetComponent<Target>(out var target)) return;
      RemoveTarget(target);
   }

   public bool SelectTarget()
   {
      if (targets.Count == 0) return false;

      Target closestTarget = null;

      var closestTargetDistance = Mathf.Infinity;
      
      foreach (var target in targets)
      {
         Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

         if (viewPos.x is < 0 or > 1 || viewPos.y is < 0 or > 1)
         {
            continue;
         }

         Vector2 centerCamera = viewPos - new Vector2(.5f, .5f);
         if (centerCamera.sqrMagnitude < closestTargetDistance)
         {
            closestTarget         = target;
            closestTargetDistance = centerCamera.sqrMagnitude;
         }

      }

      if (closestTarget == null) return false;
      
      currentTarget = closestTarget;
      cinemachineTargetGroup.AddMember(currentTarget.transform, 1f, 2f);
      return true;
   }

   public void CancelTarget()
   {
      if (currentTarget == null) return;
      cinemachineTargetGroup.RemoveMember(currentTarget.transform);
      currentTarget = null;
      
   }
   private void RemoveTarget(Target target)
   {
      if (currentTarget == target)
      {
         cinemachineTargetGroup.RemoveMember(currentTarget.transform);
         currentTarget = null;
      }

      target.OnDestroyed -= RemoveTarget;
      targets.Remove(target);
   }
   public Target GetCurrentTarget()
   {
      return currentTarget;
   }
}
