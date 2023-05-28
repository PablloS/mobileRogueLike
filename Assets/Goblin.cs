using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public float damage = 5f;

    public float moveSpeed = 500f;

    public float attackDelay = 10f;
    bool canAttack = true;

    public float goblinKnockbackForce = 10f;

    public DetectionZone protectedZone;

    public DetectionZone attackZone;

    private Transform target; 

    public AttackingItems spear; 

    bool isLooking = false;
    bool isMoving = true;
    bool detectPlayer = false;

    public float goblinHitDelay = 0.01f;

    Vector2 goblinDirection = new Vector2(90, 90).normalized; 

    Rigidbody2D rb;

    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    private void FixedUpdate()
    {
        if (detectPlayer)
        {
            Attacking(target);
        }
        else if (protectedZone.detectedObj.Count > 0)
        {
            detectPlayer = true;
            target = protectedZone.detectedObj[0].transform; 
            
        }
        else
        {
            Patrols(); 
        }
    }

    private void Attacking(Transform tmpTarget)
    {
        Vector2 direction = (tmpTarget.position - transform.position).normalized;
        RotateGoblin(direction);

        if (attackZone.detectedObj.Count > 0 && canAttack)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(GoblinAttack());
        }
        else
        {
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void Patrols()
    {
        if (!isLooking)
        {
            isMoving = true;
            RotateGoblin(goblinDirection);
            rb.AddForce(goblinDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("isMoving", isMoving);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("GoblinProtectZone") && !detectPlayer)
        {
            StartCoroutine(InspectTerritory());
        }
    }

    IEnumerator InspectTerritory()
    {
        //Осматривает территорию
        isLooking = true; 
        yield return new WaitForSeconds(2);

        //Разворачиваем гоблина и отправляем в новую точку
        int randomDirX = Random.Range(0, 90);
        int randomDirY = Random.Range(0, 90);
        Vector2 newDirection;

        if (goblinDirection.x > 0)
        {
            newDirection = new Vector2(-randomDirX, -randomDirY).normalized;
        }
        else
        {
            newDirection = new Vector2(randomDirX, randomDirY).normalized;
        }

        Vector2 center = protectedZone.GetComponent<Collider2D>().bounds.center;
        goblinDirection = ((center + (newDirection)) - rb.position).normalized;
        isLooking = false; 
    }

    IEnumerator GoblinAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(spear.attackPreparationTime);
        for (int i = 0; i < 3; i++) {
            spear.AttackItemCollider.enabled = true;
            yield return new WaitForSeconds(spear.attackTime);
            spear.AttackItemCollider.enabled = false;
            yield return new WaitForSeconds(goblinHitDelay);
        }
        spear.StopAttack(); 

        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }


    private void RotateGoblin(Vector2 direction)
    {
        Vector2 lookDirection = (rb.velocity + (direction * moveSpeed)) - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }
}
