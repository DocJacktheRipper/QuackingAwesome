using System;
using UnityEngine;
using UnityEngine.UI;

public class NestBuilding : MonoBehaviour
{
    public int numberOfSticks;
    public int neededSticks;
    
    public GameObject finishedNest;

    private Transform nbContainer;

    private void Start()
    {
        nbContainer = transform.Find("NestBuildingContainer");
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerIsTrigger(other);
    }

    private void PlayerIsTrigger(Collider other)
    {
        Inventory player = other.GetComponent<Inventory>();
        
        // does player-inventory exist?
        if (player == null)
        {
            return;
        }
        // is already built a nest on rock?
        if (nbContainer.childCount > 0)
        {
            Debug.Log("already a nest on it");
            return;
        }

        // check for sticks in duck's inventory and needed for upgrade
        if (player.numberOfSticks > 0)
        {
            //Debug.Log("Transfering sticks now");
            TransferSticks(player);
            
            PrintText();
            
            if (numberOfSticks >= neededSticks)
            {
                BuildNest();
            }
        }
    }

    private void TransferSticks(Inventory player)
    {
        // only use as much sticks as needed for the nest
        int diff = neededSticks - numberOfSticks;
        if ((diff - player.numberOfSticks) < 0)
        {
            numberOfSticks = neededSticks;
            player.numberOfSticks -= diff;
        }
        else
        {
            numberOfSticks += player.numberOfSticks;
            player.numberOfSticks = 0;
        }
    }

    private void BuildNest()
    {
        var pos = gameObject.transform.localPosition;
        pos = new Vector3(-244.35f, 0.66f, -12.6f);
        
        // create nest object
        GameObject nestOfSticks = Instantiate(finishedNest, pos, Quaternion.identity);
        // get "NestBuildingContainer" and set object as child of it
        nestOfSticks.transform.parent = nbContainer;
    }

    private void PrintText()
    {
        var text = "Sticks in Nest: " + numberOfSticks + "/" + neededSticks;
        Debug.Log(text);
    }
}
