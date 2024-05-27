using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Services;
using MainGame.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    public  float     speed        = 30;
    public  float     slowDownRate = 0.01f;
    private Rigidbody rb;
    public  float     timedelay = 5;

    private int index;

    public Collider owner;
    
    private void Update()
    {
        timedelay -= Time.deltaTime;
        if (timedelay <= 0)
        {
            transform.Recycle();
            return;
        }

        transform.position += transform.forward * (Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            if (other == owner) return;
            if (other.TryGetComponent<ForceReceiver>(out var forceReceiver))
            {
                var direction = (other.transform.position - transform.position).normalized;
                forceReceiver.AddForce(direction * 30);
            }

            if (other.TryGetComponent<PlayerStateMachine>(out var playerStateMachine))
            {
                if (playerStateMachine.CanDeflect)
                {
                    EffectManager.Instance.SpawnPerfectParry(other.ClosestPoint(transform.position));
                    gameObject.Recycle();
                }
                else
                {
                    playerStateMachine.BlockDurability.IncreaseBlock(10, isPerfectParry: false);
                }

                if (playerStateMachine.IsBlocking)
                {
                    index++;
                    if (index > 3)
                    {
                        index = 1;
                    }

                    switch (index)
                    {
                        case 1:
                            AudioService.instance.PlaySfx(SoundFXData.Deflect1);
                            break;
                        case 2:
                            AudioService.instance.PlaySfx(SoundFXData.Deflect2);
                            break;
                        case 3:
                            AudioService.instance.PlaySfx(SoundFXData.Deflect3);
                            break;
                    }
                }
            }

            if (other.TryGetComponent<EnemyStateMachine>(out var enemyStateMachine))
            {
                // EffectManager.Instance.SpawnHitEffect(other.ClosestPoint(transform.position));
                enemyStateMachine.BlockDurability.IncreaseBlock(10, isPerfectParry: false);
                
                if (enemyStateMachine.IsBlocking)
                {
                    index++;
                    if (index > 3)
                    {
                        index = 1;
                    }

                    switch (index)
                    {
                        case 1:
                            AudioService.instance.PlaySfx(SoundFXData.Deflect1);
                            break;
                        case 2:
                            AudioService.instance.PlaySfx(SoundFXData.Deflect2);
                            break;
                        case 3:
                            AudioService.instance.PlaySfx(SoundFXData.Deflect3);
                            break;
                    }
                }
                
            }
            

            EffectManager.Instance.SpawnHitEffect(other.ClosestPoint(transform.position));
            if (other.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(0);
            }
            
            gameObject.Recycle();
        }
       
    }
}