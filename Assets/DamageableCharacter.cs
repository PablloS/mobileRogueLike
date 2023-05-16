using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class DamageableCharacter : MonoBehaviour, IDamageable
{
    Animator animator;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    private bool isDeath = false;

    public bool removableCharacter = true; 

    public float health;

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                StartCoroutine(Defeated());
            }
        }
        get
        {
            return health;
        }
    }



    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDeath)
        {
            sprite.material.color = new Color(1, 1, 1, sprite.material.color.a - 0.01f);
        }
    }

    IEnumerator Defeated()
    {
        MakeUntargertable();
        animator.SetTrigger("IsDeath");
        rb.simulated = false; 
        if (removableCharacter)
        {
            float deathTime = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(deathTime);
            isDeath = true;
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
        
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        animator.SetTrigger("IsHit");
        Health -= damage;

        //Применить силу удара
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnHit(float damage)
    {
        animator.SetTrigger("IsHit");
        Health -= damage;
    }

    public void MakeUntargertable()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    private void MakeTargertable()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

}

