using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenGolem : MonoBehaviour, IEnemy
{
    public float damage = 10;
    public float moveSpeed = 350;

    public float attackDelay = 3f;
    bool canAttack = true;
    bool isLooking = false;
    bool detectPlayer = false;
    bool isMoving = false; 

    public float attackPreparationTime = 0.1f;
    public float inspectingTime = 2f; 

    Rigidbody2D rb;
    Animator animator;
    Transform target;

    public DetectionZone detectionZone;

    Vector2 golemDirection; 

    GolemProjectile projectile; 

    public float Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    public float MoveSpeed { 
        get 
        {
            return moveSpeed;
        }
        set 
        {
            moveSpeed = value; 
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        projectile = GetComponent<GolemProjectile>();
        golemDirection = new Vector2(90, 0).normalized;
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObj.Count > 0)
        {
            detectPlayer = true;
            Attacking();
        }
        else
        {
            detectPlayer = false;
            Patrolling();
        }
    }

    void Patrolling()
    {
        
        RotateEnemy(golemDirection); 
        if (!isLooking)
        {
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            rb.AddForce(golemDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
        }
    }

    private void Attacking()
    {
        isMoving = false;
        animator.SetBool("isMoving", isMoving); 
        Vector2 direction = (detectionZone.detectedObj[0].transform.position - transform.position).normalized;
        RotateEnemy(direction);
            
        if (canAttack)
        {
            target = detectionZone.detectedObj[0].transform;
            StartCoroutine(EnemyAttack());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("FrozenGolemInspectZone") && !detectPlayer)
        {
            isMoving = false; 
            StartCoroutine(InspectTerritory());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!detectPlayer)
        {
            isMoving = false;
            StartCoroutine(InspectTerritory());
        }
    }

    IEnumerator InspectTerritory()
    {
        isLooking = true; 
        yield return new WaitForSeconds(inspectingTime);
        Vector2 newDirection = transform.position;
        Vector2 center = gameObject.transform.parent.GetComponent<Collider2D>().bounds.center;
        if (golemDirection.x > 0)
        {
            newDirection = new Vector2(-transform.position.x-90, center.y).normalized;
        }
        else if (golemDirection.x <= 0)
        {
            newDirection = new Vector2(-transform.position.x + 90, center.y).normalized;
        }
        golemDirection = ((center + (newDirection)) - rb.position).normalized;
        isLooking = false; 
    }

    public IEnumerator EnemyAttack()
    {
        canAttack = false;
        animator.Play("attack");
        yield return new WaitForSeconds(attackPreparationTime); 
        projectile.SendMessage("throwProjectTile", target);
        yield return new WaitForSeconds(attackDelay);
        canAttack = true; 
    }

    public void RotateEnemy(Vector2 direction)
    {
        Vector2 lookDirection = (rb.velocity + (direction * moveSpeed)) - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }
}
