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

    void Start()
    {
        animator = GetComponent<Animator>();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory.keyCount > 0) {
                isKeyPresent = true;
            }
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            isKeyPresent = false;
        }
    }

    void Open() {
        animator.SetBool("Open", true);
        isOpened = true;
    }
}
