using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Collider2D swordCollider;
    public float damage = 3f; 

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false; 
    }

    public void Attack()
    {
        swordCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>(); 

            if (enemy != null)
            {
                enemy.Health -= damage; 
            }
        }
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }
}
