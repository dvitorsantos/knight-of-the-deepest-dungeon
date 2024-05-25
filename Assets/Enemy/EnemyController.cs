using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float stoppingDistance = 3f;
    public float followDistance = 5f;
    public int damage = 10;
    public int maxHealth = 50;
    private int currentHealth;
    public float flashDuration = 0.1f;
    public int flashCount = 3;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rididbody;
     public float knockbackForce = 10f;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa a saúde do inimigo
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer
        rididbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifica se o jogador foi atribuído
        if (player != null)
        {
            // Calcula a distância entre o inimigo e o jogador
            float distance = Vector2.Distance(transform.position, player.position);

            // Verifica se a distância está dentro do alcance de seguir e fora do alcance de parar
            if (distance < followDistance && distance > stoppingDistance)
            {
                // Calcula a direção do inimigo para o jogador
                Vector2 direction = (player.position - transform.position).normalized;

                // Move o inimigo
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
        currentHealth -= damage; // Reduz a saúde do inimigo

        rididbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Verifica se o inimigo morreu
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashDamage()); // Inicia a animação de dano
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died!");
        // Adicione aqui a lógica para quando o inimigo morrer (por exemplo, destruir o GameObject do inimigo)
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
