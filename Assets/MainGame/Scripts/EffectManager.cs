using MainGame.Utils;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>, IEffectManager
{
     public static IEffectManager Instance;

     public GameObject hitEffect;
     public GameObject perfectEff;

     public GameObject bloodStabParticle;
     protected override void Initial()
     {
          base.Initial();
          Instance = InstancePrivate;
     }

     public void SpawnHitEffect(Vector3 position)
     {
          var eff = Instantiate(hitEffect, position, Quaternion.identity);
     }

     public void SpawnPerfectParry(Vector3 position)
     {
          var eff = Instantiate(perfectEff, position, Quaternion.identity);
     }
     public void SpawnBloodStabParticle(Transform spawn)
     {
          Instantiate(bloodStabParticle, spawn.position, spawn.localRotation);
     }
     public void SpawnBloodStabParticle(Transform spawn, Quaternion rotation)
     {
          Instantiate(bloodStabParticle, spawn.position, rotation);
     }
}

public interface IEffectManager
{
     public void SpawnHitEffect(Vector3 position);

     public void SpawnPerfectParry(Vector3 position);

     public void SpawnBloodStabParticle(Transform spawn)
     {
          
     }

     public void SpawnBloodStabParticle(Transform spawn, Quaternion rotation)
     {
          
     }
}
