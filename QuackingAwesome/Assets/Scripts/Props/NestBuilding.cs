using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NestBuilding : MonoBehaviour
{
    public int numberOfSticks;
    public int neededSticks;

    public bool enableDynamicBuilding;
    public float heightForDynBuilding;
    
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


        // check for sticks in duck's inventory and needed for upgrade
        if (player.numberOfSticks > 0)
        {
            //Debug.Log("Transfering sticks now");
            TransferSticks(player);
            
            // visually showing progress (?)
            if (enableDynamicBuilding)
            {
                BuildNestDynamically();
                return;
            }
            
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
            RespawnSticksInWorld(diff);    // so there are the same amount of sticks in the world
        }
        else
        {
            numberOfSticks += player.numberOfSticks;
            RespawnSticksInWorld(player.numberOfSticks); // so there are the same amount of sticks in the world
            player.numberOfSticks = 0;
        }
    }

    private void RespawnSticksInWorld(int numberOfTransferedSticks)
    {
        GameObject spawner = GameObject.Find("SpawningBehaviour");
        if (spawner == null)
            return;
        StickSpawner sp = spawner.GetComponent<StickSpawner>();
        
        
        sp.SpawnStick(numberOfTransferedSticks);
    }

    private void BuildNestDynamically()
    {
        if (nbContainer.childCount <= 0)
        {    
            BuildNest();
        }
        
        // set y pos based on heightForDynBuilding and number of sticks in nest
        var percentageOfBeingFinished = 1 - (neededSticks - numberOfSticks) * 1.0f / neededSticks;
        Debug.Log(percentageOfBeingFinished);
        // percentageOfBeingFinished * heightForDynBuilding
        nbContainer.GetChild(0).transform.position = new Vector3(0f, 0f, 0f); 
        
    }

    private void BuildNest()
    {
        // is already built a nest on rock?
        if (nbContainer.childCount > 0)
        {
            Debug.Log("already a nest on it");
            return;
        }
        
        // create nest object
        GameObject nestOfSticks = Instantiate(finishedNest, new Vector3(0, 0, 0), Quaternion.identity);
        // get "NestBuildingContainer" and set object as child of it
        nestOfSticks.transform.parent = nbContainer;
        nestOfSticks.transform.position = nbContainer.position;
    }

    private void PrintText()
    {
        var text = "Sticks in Nest: " + numberOfSticks + "/" + neededSticks;
        Debug.Log(text);
    }
}
