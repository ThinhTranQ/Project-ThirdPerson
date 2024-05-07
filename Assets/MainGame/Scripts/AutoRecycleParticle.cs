using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Utils;
using UnityEngine;

public class AutoRecycleParticle : MonoBehaviour
{
   private ParticleSystem particleSystem;

   private void Awake()
   {
      particleSystem = GetComponent<ParticleSystem>();
   }

   private void OnEnable()
   {
      particleSystem.Play();
   }

   private void Update()
   {
      if (!particleSystem.isPlaying)
      {
         gameObject.Recycle();
      }
   }
}
