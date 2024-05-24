using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : EntityCombo
{
    // public  List<ComboList>   comboLists;
    public  List<ComboList>   phase2Combo;
    private EnemyStateMachine enemy;
    private bool              isPhase2;

    private void Start()
    {
        enemy = GetComponent<EnemyStateMachine>();
        // for testing
        ImplementCombo();
    }

    public void ChangePhaseCombo()
    {
        isPhase2 = true;
        // ChangeCombo();
        currentIndex = 0;
        ImplementCombo();
    }

    protected override void ImplementCombo()
    {
        currentCombo = isPhase2 ? phase2Combo[currentIndex] : comboLists2[currentIndex];

        currentCombo.ImplementCombo(enemy);
    }

    public Attack GetCurrentAttack(int index)
    {
        return currentCombo.attacks[index];
    }

    public override void ChangeCombo()
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
            if (currentIndex >= comboLists2.Count)
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