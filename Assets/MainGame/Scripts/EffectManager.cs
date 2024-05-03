using MainGame.Utils;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>, IEffectManager
{
     public static IEffectManager Instance;

     public GameObject hitEffect;
     public GameObject perfectEff;

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
}

public interface IEffectManager
{
     public void SpawnHitEffect(Vector3 position);

     public void SpawnPerfectParry(Vector3 position);
}