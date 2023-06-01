using System;
using UnityEngine;
using UnityEngine.Events; 

public class GlobalEventManager
{
    public static UnityEvent<int> OnCoinAdd = new UnityEvent<int>();
    public static UnityEvent OnEnemyDead = new UnityEvent();
    public static UnityEvent OnTimerEnd = new UnityEvent(); 

    public static void SendCoinAdd(int numberOfCoins)
    {
        OnCoinAdd.Invoke(numberOfCoins); 
    }

    public static void SendEnemyDead()
    {
        OnEnemyDead.Invoke(); 
    }

    public static void SendTimerEnd()
    {
        OnTimerEnd.Invoke();
    }
}


