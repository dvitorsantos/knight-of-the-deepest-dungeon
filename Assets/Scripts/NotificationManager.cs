using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public Text notificationText;
    public float displayDuration = 2f;

    private float timer;
    private bool isDisplaying;

    private void Update()
    {
        if (isDisplaying)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                HideNotification();
            }
        }
    }

    public void ShowNotification(string message)
    {
        notificationText.text = message;
        notificationText.enabled = true;
        timer = displayDuration;
        isDisplaying = true;
    }

    private void HideNotification()
    {
        notificationText.enabled = false;
        isDisplaying = false;
    }
}
