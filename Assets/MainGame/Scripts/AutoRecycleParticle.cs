using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Utils;
using UnityEngine;

public class AutoRecycleParticle : MonoBehaviour
{
   private ParticleSystem particle;

   private void Awake()
   {
      particle = GetComponent<ParticleSystem>();
   }

   private void OnEnable()
   {
      particle.Play();
   }

   private void Update()
   {
      if (!particle.isPlaying)
      {
         gameObject.Recycle();
      }
   }
}
