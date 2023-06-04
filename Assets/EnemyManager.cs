using System;
using UnityEngine;

class EnemyManager : MonoBehaviour
{
    private int enemyCount;

    private void Awake()
    {
        GlobalEventManager.OnEnemyDead.AddListener(EnemyDead);
    }
    private void Start()
    {
        foreach (Transform enemySpawner in transform)
        {
            enemyCount += enemySpawner.GetComponent<EnemyRandomSpawner>().enemyAmount;
        }
    }

    void EnemyDead()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            GlobalEventManager.SendAllEnemyDead();
        }
    }
}

