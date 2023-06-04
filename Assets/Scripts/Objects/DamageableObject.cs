using System;
using UnityEngine;
using System.Collections; 

class DamageableObject : MonoBehaviour, IDamageable
{
    public float health = 1;
    Collider2D objectCollider;
    public Animator animator;
    public bool isInvulnerability = false;
    public bool isBroken = false;

    private void Start()
    {
        objectCollider = GetComponent<Collider2D>();
    }

    public float Health { 
        get {
            return health;
        }
        set { 
            health = value;
            if (health <= 0)
            {
                StartCoroutine(Break());
            }
        } 
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!isInvulnerability)
        {
            Health -= damage;
        }
           
    }

    public void OnHit(float damage)
    {
        if (!isInvulnerability)
        {
            Health -= damage;
        }
        
    }

    public void MakeUntargertable()
    {
        objectCollider.enabled = false;
    }

    IEnumerator Break()
    {
        animator.SetTrigger("break");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isBroken = true; 
    }
}
