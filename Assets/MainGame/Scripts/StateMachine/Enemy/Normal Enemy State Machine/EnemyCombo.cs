using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : MonoBehaviour
{
    public List<ComboList> comboLists;

    public List<ComboList> phase2Combo;

    private EnemyStateMachine enemy;
    private ComboList         currentCombo;

    private int  currentIndex;
    public  bool canChangeCombo;

    private bool isPhase2;

    private void Start()
    {
        enemy = GetComponent<EnemyStateMachine>();
        // for testing
        ImplementCombo();
    }

    public void ChangePhaseCombo()
    {
        isPhase2     = true;
        ChangeCombo();
        currentIndex = 0;
    }

    private void ImplementCombo()
    {
        if (isPhase2)
        {
            currentCombo = phase2Combo[currentIndex];
        }
        else
        {
            currentCombo = comboLists[currentIndex];
        }

        currentCombo.ImplementCombo(enemy);
    }

    public Attack GetCurrentAttack(int index)
    {
        return currentCombo.attacks[index];
    }

    public void ChangeCombo()
    {
        currentIndex++;
        if (isPhase2)
        {
            if (currentIndex >= phase2Combo.Count)
            {
                currentIndex = 0;
            }
        }
        else
        {
            if (currentIndex >= comboLists.Count)
            {
                currentIndex = 0;
            }
        }
      
        

        if (!canChangeCombo)
        {
            currentIndex = 0;
        }

        ImplementCombo();
    }
}