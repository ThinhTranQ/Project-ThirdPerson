using System;
using UnityEngine;

public class BlockDurability : MonoBehaviour
{
    public  float BLOCk_MAX = 100;
    
    
    public float currentBlock = 0;

    public event Action OutOfStamina;

    public float delay;

    public float decreaseSpd;

    public float numberOfBlockBar;

    public void DecreaseBlockBar()
    {
        numberOfBlockBar -= 0;
    }
    public void IncreaseBlock(float number, bool isPerfectParry)
    {
        currentBlock += number;
        delay        =  5;
        if (currentBlock >= BLOCk_MAX && !isPerfectParry)
        {
            print("exhausted");
            currentBlock = 0;
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