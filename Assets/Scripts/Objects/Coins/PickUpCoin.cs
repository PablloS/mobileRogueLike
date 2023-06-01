using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    bool isPicked = false;
    bool haveTarget = false;
    bool haveSpawnPoint = false;
    bool isOnSpawn = false;
    public float coinsMoveSpeed = 400;

    public int value = 1; 

    GameObject target;

    Vector3 spawnPoint;
    
    Rigidbody2D rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void FixedUpdate()
    {
        if (haveTarget && isOnSpawn)
        {
            Vector2 moveDirection = (target.transform.position - transform.position).normalized;
            rb.velocity = (moveDirection*coinsMoveSpeed*Time.deltaTime);
            isPicked = CheckDistance(target.transform.position, transform.position, 0.2f);
        }
        else if (!isOnSpawn && haveSpawnPoint)
        {
            Vector2 spawnDirection = (spawnPoint - transform.position).normalized; 
            rb.velocity = (spawnDirection * coinsMoveSpeed * Time.deltaTime);
            isOnSpawn = CheckDistance(spawnPoint, transform.position, 0.01f);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 1);
        }
        if (isPicked)
        {
            GlobalEventManager.SendCoinAdd(value);
            Destroy(gameObject);
            if (transform.parent.childCount == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            
        }
    }

    void GetSpawnPoint(Vector2 point)
    {
        spawnPoint = point;
        haveSpawnPoint = true;
    }

    private bool CheckDistance(Vector2 pos1, Vector2 pos2, float precision)
    {
        bool checkX = Mathf.Abs(pos1.x - pos2.x) <= precision;
        bool checkY = Mathf.Abs(pos1.y - pos2.y) <= precision;

        return checkX && checkY; 
    }

    void GetTarget(GameObject followTarget)
    {
        target = followTarget;
        haveTarget = true;
    }


}
