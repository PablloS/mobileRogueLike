using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoins : MonoBehaviour
{
    public CoinsZone zone;
    public DamageableCharacter enemy;

    public int numberOfCoin = 10;

    void SpawnCoins()
    {
        zone.numberOfCoins = numberOfCoin; 
        Instantiate(zone, enemy.transform.position, Quaternion.identity);
        Destroy(gameObject.GetComponent<DropCoins>());
    }
}

