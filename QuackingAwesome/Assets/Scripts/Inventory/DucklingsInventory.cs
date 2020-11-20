using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingsInventory : MonoBehaviour
{
    public int DucklingCount { get; private set; }

    public void RemoveDucklings(int number)
    {
        DucklingCount -= number;
        if (DucklingCount < 0)
        {
            DucklingCount = 0;
        }
    }

    public void AddDucklings(int number)
    {
        DucklingCount += number;
    }
}
