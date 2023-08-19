using System;
using UnityEngine;

namespace MainGame.Gameplay.Combat
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private CharacterController controller;

        private Collider[]  allColliders;
        private Rigidbody[] allRigidbodies;
        
        
        private void Start()
        {
            allColliders   = GetComponentsInChildren<Collider>(true);
            allRigidbodies = GetComponentsInChildren<Rigidbody>(true);
            ToggleRagDoll(false);
        }

        public void ToggleRagDoll(bool isRagDoll)
        {
            foreach (var collider in allColliders)
            {
                if (collider.gameObject.CompareTag("Ragdoll"))
                {
                    collider.enabled = isRagDoll;
                }
            }
            
            foreach (var rigidbody in allRigidbodies)
            {
                if (rigidbody.gameObject.CompareTag("Ragdoll"))
                {
                    rigidbody.isKinematic = !isRagDoll;
                    rigidbody.useGravity  = isRagDoll;
                }
            }

            controller.enabled = !isRagDoll;
            Animator.enabled   = !isRagDoll;
        }
    }
}