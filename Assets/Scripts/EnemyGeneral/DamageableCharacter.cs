using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public TextMeshProUGUI healthText; 

    Animator animator;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    public bool isDeath = false;

    public bool removableCharacter = true;

    public float invulnerabilityTime = 2f; 
 
    public float health;

    public bool isInvulnerability = false;

    public HealthBar healthBar; 

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
        healthBar.SetMaxHeath(health); 
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
        animator.Play("death");
        rb.simulated = false; 
        if (removableCharacter)
        {
            float deathTime = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(deathTime);
            gameObject.GetComponent<DropCoins>().SendMessage("SpawnCoins");
            GlobalEventManager.SendEnemyDead(); 
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
        

        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        textTransform.SetParent(canvas.transform);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!isInvulnerability)
        {
            float characterScale = gameObject.transform.localScale.x;
            gameObject.transform.localScale = new Vector3(characterScale - 0.3f, characterScale + 0.3f, 1);
            sprite.material.color = new Color(0.9f, 0.09f, 0.27f, 1);
            Health -= damage;

            OnHitText(damage.ToString());

            //Применить силу удара
            rb.AddForce(knockback, ForceMode2D.Impulse);

            StartCoroutine(TakingDamage(characterScale));
            healthBar.SetHeath(health);
        }
        
        
    }

    public void OnHit(float damage)
    {
        if (!isInvulnerability)
        {
            float characterScale = gameObject.transform.localScale.x;
            gameObject.transform.localScale = new Vector3(characterScale + 0.2f, characterScale - 0.1f, 1);
            Health -= damage;

            OnHitText(damage.ToString());

            StartCoroutine(TakingDamage(characterScale));

            healthBar.SetHeath(health); 
        }
        
    }

    IEnumerator TakingDamage(float scale)
    {
        isInvulnerability = true; 
        yield return new WaitForSeconds(invulnerabilityTime);
        gameObject.transform.localScale = new Vector3(scale, scale, 1);
        sprite.material.color = new Color(1, 1, 1, 1);
        isInvulnerability = false;
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

