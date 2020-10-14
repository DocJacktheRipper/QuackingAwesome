using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingsInventory : MonoBehaviour
{
    public int DucklingCount { get; private set; }

    public void RemoveDucklings(int number)
    {
        DucklingCount -= number;
        if (number < 0)
        {
            DucklingCount = 0;
        }
    }
}
