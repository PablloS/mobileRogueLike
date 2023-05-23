using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyManager : MonoBehaviour
{
    public static GameObject[] enemy;

    private static void FixedUpdate()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        print(enemy.Length); 
        if (enemy.Length == 0)
        {
            GlobalEventManager.SendEnemyDeath(); 
        }
    }
}

