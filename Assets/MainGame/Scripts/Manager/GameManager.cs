using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Utils;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyStateMachine[] enemiesInStage;
    // public EnemyStateMachine   level1;
    // public EnemyStateMachine   level2;
    // public EnemyStateMachine   level3;

    public GameObject wall;

    private int index;

    private int count;
    

    private void Start()
    {
        foreach (var enemy in enemiesInStage)
        {
            enemy.Health.OnDie += OnEnemyDie;
        }

        for (int i = 0; i < enemiesInStage.Length; i++)
        {
            enemiesInStage[i].gameObject.SetActive(i == 0);
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in enemiesInStage)
        {
            enemy.Health.OnDie -= OnEnemyDie;
        }
    }

    private void OnEnemyDie()
    {
        count++;
        if (count >= enemiesInStage.Length)
        {
            UIManager.Instance.ShowGameWin();
            return;
        }
        StartCoroutine(DelaySpawnEnemy());
    }

    private IEnumerator DelaySpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        enemiesInStage[count].gameObject.SetActive(true);
    }
}