using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawner : ISpawner
{

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
