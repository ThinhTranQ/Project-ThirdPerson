using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public EnemyStateMachine level1;
   public EnemyStateMachine level2;
   public EnemyStateMachine level3;

   public GameObject wall;

   private int index;

   private int count;
   
   private void Start()
   {
      level1.Health.OnDie += OnEnemyDie;
      level2.Health.OnDie += OnEnemyDie;
      level3.Health.OnDie += OnEnemyDie;
      
      level2.gameObject.SetActive(false);
      level3.gameObject.SetActive(false);

   }

   private void OnDestroy()
   {
      level1.Health.OnDie -= OnEnemyDie;
      level2.Health.OnDie -= OnEnemyDie;
      level3.Health.OnDie -= OnEnemyDie;
   }

   private void OnEnemyDie()
   {
      count++;
      StartCoroutine(DelaySpawnEnemy());
      
      if (!level2.gameObject.activeInHierarchy)
      {
         level2.gameObject.SetActive(true);
         return;
      }
      
      if (!level3.gameObject.activeInHierarchy)
      {
         level3.gameObject.SetActive(true);
         return;
      }

      if (count >= 3)
      {
         UIManager.Instance.ShowGameWin();
      }
      
   }

   private IEnumerator DelaySpawnEnemy()
   {
      yield return new WaitForSeconds(3f);
      if (!level2.gameObject.activeInHierarchy)
      {
         level2.gameObject.SetActive(true);
         yield break;
      }

      if (!level3.gameObject.activeInHierarchy)
      {
         level3.gameObject.SetActive(true);
         wall.SetActive(false);
         yield break;
      }
   }
}
