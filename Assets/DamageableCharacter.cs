using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
class DamageableCharacter : MonoBehaviour, IDamageable
{
    public TextMeshProUGUI healthText; 

    Animator animator;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    public bool isDeath = false;

    public bool removableCharacter = true; 

    public float health;

    public bool isDamageble = true; 

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
        Destroy(transform.Find("Shadow").gameObject);
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

    private void OnHitText(string damage)
    {
        healthText.SetText(damage);
        RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();

        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        

        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        textTransform.SetParent(canvas.transform);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (isDamageble) {
            animator.SetTrigger("IsHit");
            Health -= damage;

            OnHitText(damage.ToString());

            //Применить силу удара
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
        
    }

    public void OnHit(float damage)
    {
        if (isDamageble)
        {
            animator.SetTrigger("IsHit");
            Health -= damage;

            OnHitText(damage.ToString());
        }
        
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

