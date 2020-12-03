using System;
using System.Collections;
using System.Collections.Generic;
using AI.Beaver.StateMachine_Beaver;
using Controllers.Buttons.NestMenu;
using Nest;
using Nest.NestMenu;
using UnityEngine;

public class MultipleNestsHandler : MonoBehaviour
{
    public Transform nestParent;
    
    public NestButton nestButton;
    public EggButtons eggButtons;

    public Upgrades upgrades;
    public NestInfoText nestInfoText;

    private void Start()
    {
        upgrades.nestsParent = nestParent;
    }

    public void ApplyClosestNest(GameObject nest)
    {
        nestButton.nest = nest.GetComponent<NestBuilding>();
        
        var layAndHatchEgg = nest.GetComponent<LayAndHatchEgg>();
        eggButtons.nest = layAndHatchEgg;
        nestInfoText.nest = layAndHatchEgg;
    }
}
