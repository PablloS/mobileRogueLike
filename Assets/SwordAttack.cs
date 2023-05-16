using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 3f;

    public float knockbackForce = 150f; 

    // Start is called before the first frame update
    void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("sword collider not set"); 
        }
    }

    public void Attack()
    {
        swordCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy") && !other.CompareTag("DetectionZone"))
        {

            IDamageable damageableObject = other.GetComponent<IDamageable>(); 

            Vector3 parentPosition = transform.parent.position;
            Vector2 direction = (other.gameObject.transform.position - parentPosition).normalized;

            Vector2 knockback = direction * knockbackForce;

            damageableObject.OnHit(damage, knockback); 
        }
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }
}
