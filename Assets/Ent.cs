using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour
{

    public float damage = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>(); 

        if (damageable != null && collision.collider.CompareTag("Player"))
        {
            damageable.OnHit(damage); 
        }
    }
}
