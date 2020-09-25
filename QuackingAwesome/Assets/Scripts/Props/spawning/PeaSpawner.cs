using System.Collections;
using System.Collections.Generic;
using Props.spawning;
using UnityEngine;

public class PeaSpawner : Spawner
{
    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnStick(minItemsInWorld);
        }
    }
    public void Reset()
    {
        SpawnObject();
    }

    public void SpawnStick()
    {
        SpawnObject();
    }

    public void SpawnStick(int numberOfSticks)
    {
        for (int i = 0; i < numberOfSticks; i++)
        {
            SpawnObject();
        }
    }
}
