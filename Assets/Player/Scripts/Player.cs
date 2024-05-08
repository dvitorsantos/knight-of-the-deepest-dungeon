using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool attacking = false;

    private float moveSpeed = 5f;

    private float attackSpeed = 0.25f;
    private float attackTimer = 0f;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

     void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.F))
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
    }
}
