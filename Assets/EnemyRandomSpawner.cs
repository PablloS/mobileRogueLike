using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawner : MonoBehaviour
{
    public GameObject[] enemy;
    
    public int enemyAmount = 3;

    private List<Transform> spawnPoints;

    private void Awake()
    {
        GlobalEventManager.EnemyDeath.AddListener(SpawnEnemy); 
    }

    void Start()
    {
        SpawnEnemy(); 
    }

    private void SpawnEnemy()
    {
        spawnPoints = new List<Transform>(transform.GetComponentsInChildren<Transform>());
        for (int i = 0; i < enemyAmount; i++)
        {
            int randomEnemy = Random.Range(0, enemy.Length);
            int randomPoints = Random.Range(0, spawnPoints.Count);

            Instantiate(enemy[randomEnemy], spawnPoints[randomPoints].transform.position, Quaternion.identity);

            spawnPoints.RemoveAt(randomPoints);
        }
    }
}
