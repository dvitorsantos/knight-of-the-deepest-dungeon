using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool attacking = false;

    private float moveSpeed = 5f;

    private float attackSpeed = 0.25f;
    private float attackTimer = 0f;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    public NotificationManager notificationManager;

    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;

    public float flashDuration = 0.1f; 
    public int flashCount = 3;
    private float attackRadius = 2.0f;

    private bool isDead = false;

     void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        notificationManager = FindObjectOfType<NotificationManager>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        movement = movement.normalized;

        rigidbody.velocity = movement * moveSpeed;

        if (moveHorizontal != 0 || moveVertical != 0) {
            animator.SetBool("Running", true);
        } else {
            animator.SetBool("Running", false);
        }

         if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true; // Inverte o sprite horizontalmente
        }
        else if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false; // Reseta a orientação horizontal do sprite
        }

        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attacking", true);
        }

        if (animator.GetBool("Attacking") == true) {
       
             attackTimer += Time.deltaTime;
          
             if (attackTimer >= attackSpeed) {
                Attack();
             }
        }
    }

    private void Attack() {
        attacking = true;
        animator.SetBool("Attacking", false);
        attackTimer = 0.0f;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRadius, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Vector2 knockbackDirection = (hit.transform.position - transform.position).normalized;

                // Aplica dano ao inimigo
                EnemyController enemyHealth = hit.collider.GetComponent<EnemyController>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage, knockbackDirection);
                }
            }
        }
    }

    public void Heal() {
        if (currentHealth < maxHealth) {
            currentHealth += 20;
            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
        }
    }

     public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        notificationManager.ShowNotification("Life: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(Die());
        } else {
            StartCoroutine(FlashDamage());
        }
    }

    private IEnumerator Die() {
        isDead = true;
        animator.SetTrigger("Dead");
        rigidbody.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private IEnumerator FlashDamage()
    {
        Color originalColor = spriteRenderer.color; // Armazena a cor original do sprite

        for (int i = 0; i < flashCount; i++)
        {
            // Altera a cor do sprite para vermelho
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration); // Aguarda pela duração do flash

            // Altera a cor do sprite para branco
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration); // Aguarda pela duração do flash
        }

        // Retorna a cor original do sprite
        spriteRenderer.color = originalColor;
    }
}
