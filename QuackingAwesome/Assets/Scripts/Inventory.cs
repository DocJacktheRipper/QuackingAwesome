using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int numberOfSticks = 0;
    public int maxCapacityOfSticks = 1;    
    
    public ProgressBar Pb;
    
    // Display (debugging purpose)
    public Text display;

    private void Start()
    {
        Pb.BarValue = 10f;
    }

    private void Update()
    {
        display.text = "Collected Sticks: " + numberOfSticks + "/" + maxCapacityOfSticks;
    }
}
