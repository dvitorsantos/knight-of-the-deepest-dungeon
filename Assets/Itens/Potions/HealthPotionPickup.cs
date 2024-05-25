using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionPickup : MonoBehaviour
{
    public NotificationManager notificationManager;

    void Start() {
        notificationManager = FindObjectOfType<NotificationManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Heal();
            notificationManager.ShowNotification("+20 Health");
            Destroy(gameObject);
        }
    }
}
