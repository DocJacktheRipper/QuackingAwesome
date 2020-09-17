using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NestBuilding : MonoBehaviour
{
    public int numberOfSticks = 0;
    public int neededSticks;
    public Text display;

    public GameObject finishedNest;

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

            display.text = "Sticks in Nest: " + numberOfSticks + "/" + neededSticks;
            if (numberOfSticks >= neededSticks)
            {
                Vector3 pos = this.gameObject.transform.position;
                //pos = pos + new Vector3(0, 5, 0);
                Instantiate(finishedNest, pos, Quaternion.identity);
            }
        }
    }
}
