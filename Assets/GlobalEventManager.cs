using System;
using UnityEngine;
using UnityEngine.Events; 

public class GlobalEventManager
{
    public static UnityEvent EnemyDeath = new UnityEvent(); 

    public static void SendEnemyDeath()
    {
        EnemyDeath.Invoke(); 
    }
}


