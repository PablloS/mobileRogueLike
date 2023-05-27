using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    public bool canMove = true;

    Vector2 movementInput = Vector2.zero;
    Rigidbody2D rb;
    Animator animator;

    public AttackingItems swordAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero && canMove)
        {
            //rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed); 
            rb.AddForce(movementInput * moveSpeed * Time.deltaTime);

            if (rb.velocity.magnitude > maxSpeed)
            {
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }

            animator.SetBool("isMoving", true);
            RotatePlayer(movementInput);
            
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction); 
            animator.SetBool("isMoving", false);
        }
    }

    private void RotatePlayer(Vector2 direction)
    {
        Vector2 lookDirection = (rb.velocity + (movementInput * moveSpeed)) - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    } 

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>(); 
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        canMove = false;
        swordAttack.Attack();
    }

    public void StopSwordAttack()
    {
        canMove = true;
        swordAttack.StopAttack();
    }
}
