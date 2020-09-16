using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestBuilding : MonoBehaviour
{
    public int numberOfSticks = 0;

    private void OnTriggerEnter(Collider other)
    {
        PlayerIsTrigger(other);
    }

    private void PlayerIsTrigger(Collider other)
    {
        Inventory player = other.GetComponent<Inventory>();

        if (player == null)
        {
            return;
        }

        if (player.numberOfSticks > 0)
        {
            numberOfSticks += player.numberOfSticks;
            player.numberOfSticks = 0;
        }
    }
}
