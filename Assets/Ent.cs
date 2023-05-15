using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour, IDamageable
{

    Animator animator;
    SpriteRenderer sprite;
    Rigidbody2D rb; 
    public bool isDeath = false;


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

    public float health;

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
            sprite.color = new Color(sprite.color.r, sprite.color.b, sprite.color.g, sprite.color.a - 0.01f); 
        }
    }

    IEnumerator Defeated()
    {
        MakeUntargertable(); 
        animator.Play("Ent_death");
        float deathTime = animator.GetCurrentAnimatorStateInfo(0).length; 
        yield return new WaitForSeconds(deathTime);
        isDeath = true;
        yield return new WaitForSeconds(3);
        Destroy(gameObject); 
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;

        //Применить силу удара
        rb.AddForce(knockback);
    }

    public void OnHit(float damage)
    {
        Health -= damage; 
    }

    public void MakeUntargertable()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
