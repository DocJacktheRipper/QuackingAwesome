using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStickDelete : MonoBehaviour
{
    
    public GameObject spawnPositionPrefab;
    
    private bool isQuitting = false;
    
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    // add position too pool of spawn-points
    private void OnDestroy()
    {
        if (isQuitting) return;
        Debug.Log("Stick is destroyed.");
        
        // get pool of spawn points
        GameObject pool = GameObject.Find("StickSpawnSpots");
        
        // create new spawn point
        GameObject sPoint = Instantiate(spawnPositionPrefab, transform.position, Quaternion.identity);

        // set it as child of the pool
        sPoint.transform.parent = pool.transform;
    }
}
