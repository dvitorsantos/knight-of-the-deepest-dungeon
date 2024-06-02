using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float stoppingDistance = 0.25f;
    public float followDistance = 7f;
    public int damage = 10;
    public int maxHealth = 30;
    private int currentHealth;
    public float flashDuration = 0.1f;
    public int flashCount = 3;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Animator animator;
    public float knockbackForce = 10f;
    private float decelerationRate = 15.0f;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rigidbody.velocity -= rigidbody.velocity.normalized * decelerationRate * Time.deltaTime;

        if (player != null && !isDead)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance < followDistance && distance > stoppingDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;

                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

     public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage;

        rigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(FlashDamage());
        }
    }

    private IEnumerator Die()
    {
        animator.SetBool("Dead", true);
        isDead = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private IEnumerator FlashDamage()
    {
        Color originalColor = spriteRenderer.color;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }

        spriteRenderer.color = originalColor;
    }
}
