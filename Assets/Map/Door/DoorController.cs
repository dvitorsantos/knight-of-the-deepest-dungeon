using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isPlayerNear = false;
    private bool isOpened = false;
    private bool isKeyPresent = false;
    public bool isLocked = true;

    private Animator animator;
    public NotificationManager notificationManager;
    private BoxCollider2D collider;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isOpened && isLocked) {
            if (isKeyPresent) {
                Open();
            } else {
                notificationManager.ShowNotification("Door is locked.");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision Door!");
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.gameObject.GetComponent<PlayerInventory>();
            if (playerInventory.keyCount > 0) {
                isKeyPresent = true;
            }
            isPlayerNear = true;
        }
    }

    void OnCollisionExit2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
            isKeyPresent = false;
        }
    }

    void Open() {
        animator.SetBool("Open", true);
        isOpened = true;
        collider.enabled = false;
    }
}
