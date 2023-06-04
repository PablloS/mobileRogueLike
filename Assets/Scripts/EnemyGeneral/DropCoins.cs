using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoins : MonoBehaviour
{
    public CoinsZone zone;

    public int numberOfCoin = 10;

    void SpawnCoins()
    {
        zone.numberOfCoins = numberOfCoin; 
        Instantiate(zone, transform.position, Quaternion.identity);
        Destroy(gameObject.GetComponent<DropCoins>());
    }
}

