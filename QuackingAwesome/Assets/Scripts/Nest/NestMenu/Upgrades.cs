using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public Button dashUpgrade;
    public Button beakUpgrade;

    private DucklingsInventory ducklings;
    private DashingBehaviour dashingBehaviour;
    private StickInventory stickInventory;
    private int _neededAmountForDash;

    public int NeededAmountForDash { get; set; }
    public int NeededAmountForBeak { get; set; }

    private void Start()
    {
        var d = GameObject.Find("Duck");
        ducklings = d.GetComponent<DucklingsInventory>();
        stickInventory = d.GetComponent<StickInventory>();

        dashingBehaviour = d.GetComponent<DashingBehaviour>();
    }

    private void Update()
    {
        // Dash cooldown
        //dashUpgrade.enabled = ducklings.DucklingCount >= NeededAmountForDash;
        dashUpgrade.interactable = ducklings.DucklingCount >= NeededAmountForDash;

        // Beak capacity
        beakUpgrade.interactable = ducklings.DucklingCount >= NeededAmountForBeak;
    }


    /******** Button functions ********/
    public void UpgradeDashCooldown(float amount)
    {
        dashingBehaviour.cooldown -= amount;
        ducklings.RemoveDucklings(NeededAmountForDash);

        NeededAmountForDash += 1;
        var text = dashUpgrade.transform.Find("RequirementText").GetComponent<Text>();
        text.text = "Cost: " + NeededAmountForDash + " ducklings";
    }

    public void UpgradeCarryCapacity(int amount)
    {
        stickInventory.maxCapacityOfSticks += amount;
        Debug.Log("New capacity: " + stickInventory.maxCapacityOfSticks);
        ducklings.RemoveDucklings(NeededAmountForBeak);

        NeededAmountForBeak += 1;
    }
}
