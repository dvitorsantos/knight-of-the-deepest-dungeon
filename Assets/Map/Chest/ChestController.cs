using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private Transform transform;

    public GameObject keyPrefab;
    private Rigidbody2D keyRigidBody;
    private GameObject key;
    private float decelerationRate = 5.0f;

    private bool isPlayerNear = false;
    private bool isOpened = false;

    void Start() {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (key != null) {
            keyRigidBody.velocity -= keyRigidBody.velocity.normalized * decelerationRate * Time.deltaTime;
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            Open();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    void Open()
    {
        key = Instantiate(keyPrefab, transform.position, transform.rotation);
        keyRigidBody = key.GetComponent<Rigidbody2D>();
        Vector2 forceDirection = Vector2.down;
        keyRigidBody.gravityScale = 0;
        keyRigidBody.drag = 0;
        keyRigidBody.AddForce(forceDirection * 5.0f, ForceMode2D.Impulse);

        animator.SetBool("Open", true);
        isOpened = true;
    }
}
