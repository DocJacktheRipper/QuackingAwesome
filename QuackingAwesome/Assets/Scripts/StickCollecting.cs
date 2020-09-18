using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class StickCollecting : MonoBehaviour
{
    public int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        PlayerIsTrigger(other);
    }

    // Checks if player was trigger. If so, checks if the duck can carry more sticks.
    // If so, collect it. Otherwise, leave it.
    private void PlayerIsTrigger(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.Log("It wasn't the Duck!");
            return;
        }

        if (inventory.numberOfSticks < inventory.maxCapacityOfSticks)
        {
            inventory.numberOfSticks++;
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Can't collect any more sticks. I'm a frikkin duck!");
        }
    }
}
