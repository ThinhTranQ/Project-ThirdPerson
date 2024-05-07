using MainGame.Utils;
using Unity.Mathematics;
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
        var eff = hitEffect.Spawn(position, quaternion.identity);
    }

    public void SpawnPerfectParry(Vector3 position)
    {
        var eff = perfectEff.Spawn(position, quaternion.identity);
    }

    public void SpawnBloodStabParticle(Transform spawn)
    {
        var eff = bloodStabParticle.Spawn(spawn.position, spawn.localRotation);
    }

    public void SpawnBloodStabParticle(Transform spawn, Quaternion rotation)
    {
        var eff = bloodStabParticle.Spawn(spawn.position, rotation);
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