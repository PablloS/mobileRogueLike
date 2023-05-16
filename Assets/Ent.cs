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
            if (attackZone.detectedObj.Count > 0)
            {
                animator.SetTrigger("EntAttack");
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
            damageable.OnHit(damage); 
        }
    }

    public void EntAttack()
    {
        entAttack.Attack();
    }

    public void StopEntAttack()
    {
        entAttack.StopAttack();
    }
}
