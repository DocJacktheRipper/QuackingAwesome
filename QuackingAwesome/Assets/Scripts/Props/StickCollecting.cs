﻿using Inventory;
using UnityEngine;

namespace Props
{
    public class StickCollecting : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
        }

        // Checks if player was trigger. If so, checks if the duck can carry more sticks.
        // If so, collect it. Otherwise, leave it.
        private bool PlayerIsTrigger(Collider other)
        {
            StickInventory inventory = other.GetComponent<StickInventory>();

            if (inventory == null)
            {
                Debug.Log("It wasn't the Duck! (stick)");
                return false;
            }

            if (inventory.AddStick())
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Can't collect any more sticks. I'm a frikkin duck!");
            }

            return true;
        }
    }
}
