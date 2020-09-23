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
    

    internal void SpawnObject()
    {
        var index = (int) Random.Range(0, spawningPoints.Count);

        SpawnPoint point = spawningPoints[index];
        GameObject obj = Instantiate(spawnObject, point.transform.position, Quaternion.identity);
        //spawningPoints[index].blockedPosition = true;
        spawningPoints.Remove(point);
        Destroy(point);

        var transformEulerAngles = obj.transform.eulerAngles;
        transformEulerAngles.x = Random.Range(0, 360);
    }
}
