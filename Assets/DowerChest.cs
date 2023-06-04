using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowerChest : MonoBehaviour
{
    DamageableObject dowerChest;

    private void Awake()
    {
        GlobalEventManager.OnAllEnemyDead.AddListener(OpenChest); 
    }

    void Start()
    {
        dowerChest = gameObject.GetComponent<DamageableObject>(); 
    }

    void FixedUpdate()
    {
        if (dowerChest.isBroken)
        {
            gameObject.GetComponent<DropCoins>().SendMessage("SpawnCoins");
            Destroy(gameObject.GetComponent<DowerChest>()); 
        }
    }

    void OpenChest()
    {
        dowerChest.isInvulnerability = false;
    }
}
