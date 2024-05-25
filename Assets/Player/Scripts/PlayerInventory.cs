using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int keyCount = 0;

    public void AddKey()
    {
        keyCount++;
    }

    public void UseKey()
    {
        if (keyCount > 0) {
            keyCount--;
        }
    }
}
