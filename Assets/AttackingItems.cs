using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingItems : MonoBehaviour
{
    public Collider2D AttackItemCollider;

    public string tagTarger = "Enemy"; 
    public float damage = 3f;

    public float knockbackForce = 150f;

    // Start is called before the first frame update
    void Start()
    {
        if (AttackItemCollider == null)
        {
            Debug.LogWarning("AttackItemCollider not set");
        }
    }

    public void Attack()
    {
        AttackItemCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag(tagTarger) && !other.CompareTag("DetectionZone"))
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
        AttackItemCollider.enabled = false;
    }
}
