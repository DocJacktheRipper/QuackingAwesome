using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public Button dashUpgrade;

    private DucklingsInventory ducklings;
    private DashingBehaviour dashingBehaviour;

    private int neededAmountforDash = 1;

    private void Start()
    {
        var d = GameObject.Find("Duck");
        ducklings = d.GetComponent<DucklingsInventory>();

        dashingBehaviour = d.GetComponent<DashingBehaviour>();
    }

    private void Update()
    {
        if(ducklings.DucklingCount >= neededAmountforDash)
        {
            dashUpgrade.enabled = true;
        }
        else
        {
            dashUpgrade.enabled = false;
        }
    }


    /******** Button functions ********/
    public void UpgradeDashCooldown(float amount)
    {
        dashingBehaviour.cooldown -= amount;
        ducklings.RemoveDucklings(neededAmountforDash);
    }
}
