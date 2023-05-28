using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour
{

    public float damage = 5f;

    public DetectionZone detectionZone;

    public DetectionZone attackZone;

    public float moveSpeed = 150f;

    private bool isMoving = false;

    public AttackingItems entAttack;

    public float attackDelay = 10f;
    bool canAttack = true;

    public float entKnockbackForce = 10f; 

    Rigidbody2D rb;

    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObj.Count > 0)
        {
            if (attackZone.detectedObj.Count > 0 && canAttack)
            {
                animator.SetTrigger("EntAttack");
                StartCoroutine(EntAttack());

            }
            else
            {
                Vector2 direction = (detectionZone.detectedObj[0].transform.position - transform.position).normalized;
                isMoving = true;
                animator.SetBool("IsMoving", isMoving);
                RotateEnt(direction);
                rb.AddForce(direction * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            isMoving = false;
            animator.SetBool("IsMoving", isMoving);
        }
    }

    private void RotateEnt(Vector2 direction)
    {
        Vector2 lookDirection = (rb.velocity + (direction * moveSpeed)) - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();

        if (damageable != null && collision.collider.CompareTag("Player"))
        {
            Vector3 parentPosition = transform.position;
            Vector2 direction = (collision.gameObject.transform.position - parentPosition).normalized;

            Vector2 knockback = direction * entKnockbackForce;
            damageable.OnHit(damage, knockback);
        }
    }

    IEnumerator EntAttack()
    {
        StartCoroutine(entAttack.Attack());
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
