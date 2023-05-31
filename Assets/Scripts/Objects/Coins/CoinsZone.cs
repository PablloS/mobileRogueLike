using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsZone : MonoBehaviour
{

    public int numberOfCoins = 5;
    CircleCollider2D circleCollider;

    public GameObject coin;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        for (int i = 0; i < numberOfCoins; i++)
        {
            float radius = circleCollider.radius-circleCollider.radius/10;
            Vector2 position = circleCollider.transform.position;

            float randomY = Random.Range(-radius, radius);
            float randomX = Random.Range(-radius, radius);

            Vector2 spawnPosition = new Vector3(position.x + randomX, position.y + randomY);
            GameObject newCoin = Instantiate(coin, new Vector3(position.x, position.y, 1), Quaternion.identity);

            newCoin.transform.SetParent(gameObject.transform);
            newCoin.SendMessage("GetSpawnPoint", spawnPosition); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Transform child in transform)
            {
                child.SendMessage("GetTarget", collision.gameObject);

            }

        }
    }

}
