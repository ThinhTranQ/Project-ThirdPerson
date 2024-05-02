using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : MonoBehaviour
{
     public List<ComboList> comboLists;

     private EnemyStateMachine enemy;
     private ComboList         currentCombo;

     private int currentIndex;
     
     private void Start()
     {
          enemy = GetComponent<EnemyStateMachine>();
          // for testing
          ImplementCombo();
     }

     private void ImplementCombo()
     {
          currentCombo = comboLists[currentIndex];
          currentCombo.ImplementCombo(enemy);
     }
     
     public Attack GetCurrentAttack(int index)
     {
          return currentCombo.attacks[index];
     }

     public void ChangeCombo()
     {
          currentIndex++;
          if (currentIndex >= comboLists.Count)
          {
               currentIndex = 0;
          }

          ImplementCombo();

     }
}