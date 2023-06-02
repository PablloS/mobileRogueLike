using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawner : MonoBehaviour
{
    public GameObject[] enemy;
    
    public int enemyAmount = 3;

    private List<Transform> spawnPoints;

    void Start()
    {
        SpawnEnemy(); 
    }

    private void SpawnEnemy()
    {
        spawnPoints = new List<Transform>(gameObject.GetComponentsInChildren<Transform>());

        for (int i = 0; i < enemyAmount; i++)
        {
            int randomEnemy = Random.Range(0, enemy.Length);
            int randomPoints = Random.Range(1, spawnPoints.Count);

            Instantiate(enemy[randomEnemy], spawnPoints[randomPoints].transform.position, Quaternion.identity);

            spawnPoints.RemoveAt(randomPoints);
        }
    }
}
