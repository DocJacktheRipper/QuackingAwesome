using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingsInventory : MonoBehaviour
{
    public int ducklingCount;

    private void Start()
    {
        ducklingCount = GlobalControl.Instance.savedPlayerData.savedInventoryData.ducklings;
    }

    public void RemoveDucklings(int number)
    {
        ducklingCount -= number;
        if (ducklingCount < 0)
        {
            ducklingCount = 0;
        }
    }

    public void AddDucklings(int number)
    {
        ducklingCount += number;
    }

    private void OnDestroy()
    {
        GlobalControl.Instance.savedPlayerData.savedInventoryData.ducklings = ducklingCount;
    }
}
