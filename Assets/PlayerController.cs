using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f; 

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator; 
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput); 

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y)); 
                }
            }

            animator.SetBool("isMoving", success);
            rotatePlayer(movementInput); 
            
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void rotatePlayer(Vector2 direction)
    {
        Vector3 playerRotation = transform.eulerAngles; 

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

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                movementInput, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

        if (count == 0)
        {
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false; 
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>(); 
    }
}
