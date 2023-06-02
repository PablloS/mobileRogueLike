using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage = 5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Destroy(gameObject);

        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy"))
        {
            IDamageable damageableObject = collision.collider.GetComponent<IDamageable>();
            damageableObject.OnHit(damage); 
        }
    }
}
