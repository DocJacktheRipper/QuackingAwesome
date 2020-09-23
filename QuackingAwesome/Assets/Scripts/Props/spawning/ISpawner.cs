using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISpawner : MonoBehaviour
{
    // how many of them should be in the world at the same time
    public int minItemsInWorld;
    public int maxItemsInWorld;

    public GameObject spawnObject;

    // time between spawns
    public float spawningDelay;

    // positions to spawn
    public List<SpawnPoint> spawningPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SpawnObject()
    {
        var index = (int) Random.Range(0, spawningPoints.Count);

        GameObject obj = Instantiate(spawnObject, spawningPoints[index].transform.position, Quaternion.identity);
        spawningPoints[index].blockedPosition = true;

        var transformEulerAngles = obj.transform.eulerAngles;
        transformEulerAngles.x = Random.Range(0, 360);
    }
}
