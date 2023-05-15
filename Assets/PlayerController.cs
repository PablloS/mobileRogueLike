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
    //Поворот игрока в нужную сторону
    Vector3 playerRotation; 
    Rigidbody2D rb;
    Animator animator; 

    public SwordAttack swordAttack; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerRotation = transform.eulerAngles; 
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero && canMove)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed); 

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
        int zDirection = 0; 

        if (direction.y > 0)
        {
            if (direction.x == 0)
            {
                zDirection = 0;
            }
            else if (direction.x > 0)
            {
                zDirection = -45;
            }
            else if (direction.x < 0)
            {
                zDirection = 45;
            }
        }
        else if (direction.y < 0)
        {
            if (direction.x == 0)
            {
                zDirection = 180;
            }
            else if (direction.x > 0)
            {
                zDirection = -135;
            }
            else if (direction.x < 0)
            {
                zDirection = 135;
            }
        }
        else if (direction.y == 0)
        {
            if (direction.x > 0)
            {
                zDirection = -90; 
            }
            else
            {
                zDirection = 90;
            }
        }

        playerRotation.z = zDirection;
        transform.eulerAngles = playerRotation; 
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
