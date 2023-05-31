using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingItems : MonoBehaviour
{
    public Collider2D AttackItemCollider;

    public string tagTarged = "Enemy"; 
    public float damage = 3f;

    public float knockbackForce = 150f;

    public float attackPreparationTime = 0.1f; 
    public float attackTime = 0.1f; 

    // Start is called before the first frame update
    void Start()
    {
        if (AttackItemCollider == null)
        {
            Debug.LogWarning("AttackItemCollider not set");
        }
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackPreparationTime);
        AttackItemCollider.enabled = true;
        yield return new WaitForSeconds(attackTime);
        StopAttack(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag(tagTarged))
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
