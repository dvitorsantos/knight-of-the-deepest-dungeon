using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
     public float enableDuration = 2.0f; // Duração em segundos que o spike fica ativado
    public float disableDuration = 2.0f; // Duração em segundos que o spike fica desativado
    public int damage = 10; // Dano causado ao jogador

    private Animator animator;
    private bool isEnabled = false; // Indica se o spike está ativado

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtém o Animator
        StartCoroutine(ToggleSpike()); // Inicia a corrotina para alternar a animação do spike
    }

    private IEnumerator ToggleSpike()
    {
        while (true)
        {
            animator.SetBool("Enabled", true);
            isEnabled = true;
            yield return new WaitForSeconds(enableDuration);

            animator.SetBool("Enabled", false);
            isEnabled = false;
            yield return new WaitForSeconds(disableDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnabled && other.CompareTag("Player"))
        {
            PlayerController playerHealth = other.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isEnabled && other.CompareTag("Player"))
        {
            PlayerController playerHealth = other.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
