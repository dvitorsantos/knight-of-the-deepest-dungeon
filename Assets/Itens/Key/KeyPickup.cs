using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public NotificationManager notificationManager;

    void Start() {
        notificationManager = FindObjectOfType<NotificationManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            playerInventory.AddKey();
            notificationManager.ShowNotification("1x Key");
            Destroy(gameObject);
        }
    }
}
