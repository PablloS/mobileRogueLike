using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : PlayerAttackBehavior
{
    Animator animator;
    public Transform firePoint;
    public LineRenderer line; 
    PlayerController pc;
    public GameObject bulletEffect; 

    public float attackPreparationTime = 1; 
    public float damage = 5;
    public float knockbackForce = 10;
    public float delay = 2;
    private bool canShot = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
    }

    public override void Attack()
    {
        if (canShot)
        {
            StartCoroutine(Shot());
        }    
    }

    IEnumerator Shot()
    {
        float originalMoveSpeed = pc.moveSpeed;
        pc.moveSpeed = originalMoveSpeed / 2.5f; 

        animator.Play("shoot");

        StartCoroutine(ShotDelay()); 

        yield return new WaitForSeconds(attackPreparationTime);

        RaycastHit2D hitInfo = TargetSearch();

        if (hitInfo)
        {
            IDamageable damageableObject = hitInfo.transform.GetComponent<IDamageable>();

            if (damageableObject != null && !hitInfo.collider.CompareTag("Player"))
            {
                Vector2 position = transform.position; 
                Vector2 direction = (hitInfo.point - position).normalized;
                Vector2 knockback = direction * knockbackForce; 
                damageableObject.OnHit(damage, knockback);
            }

            line.SetPosition(0, firePoint.position);
            line.SetPosition(1, hitInfo.point);

            StartCoroutine(BulletEffect(hitInfo.point)); 
        }
        else
        {
            line.SetPosition(0, firePoint.position);
            Vector2 startPoint = firePoint.position; 
            line.SetPosition(1, (startPoint + pc.lookDirection));
        }
        pc.moveSpeed = originalMoveSpeed; 

        line.enabled = true;
        yield return new WaitForSeconds(0.02f);
        line.enabled = false; 
    }

    IEnumerator ShotDelay()
    {
        canShot = false;
        yield return new WaitForSeconds(delay);
        canShot = true;
    }

    IEnumerator BulletEffect(Vector2 spawnPoint)
    {
        GameObject effect = Instantiate(bulletEffect, spawnPoint, Quaternion.identity);
        float animationTime = effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length; 
        yield return new WaitForSeconds(animationTime);
        Destroy(effect); 
    }

    private RaycastHit2D TargetSearch()
    {
        RaycastHit2D target = new RaycastHit2D(); 
        RaycastHit2D[] hitInfoArray = Physics2D.RaycastAll(firePoint.position, pc.lookDirection, 2f);
 
        foreach (RaycastHit2D rh in hitInfoArray)
        {
            if (!rh.collider.isTrigger)
            {
                target = rh;
                break;
            }
        }

        return target; 
    }
}
