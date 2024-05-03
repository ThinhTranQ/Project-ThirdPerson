using System;
using UnityEngine;

public class BlockDurability : MonoBehaviour
{
    private const float BLOCk_MAX = 100;

    public float currentBlock = 0;

    public event Action OutOfStamina;

    public float delay;

    public float decreaseSpd;
    
    public void IncreaseBlock(float number, bool isPerfectParry)
    {
        currentBlock += number;
        delay        =  5;
        if (currentBlock >= BLOCk_MAX && !isPerfectParry)
        {
            print("exhausted");
            OutOfStamina?.Invoke();
        }
    }

    private void Update()
    {
        if (delay >= 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        currentBlock -= Time.deltaTime * decreaseSpd;
        if (currentBlock <= 0)
        {
            currentBlock = 0;
        }


    }
}