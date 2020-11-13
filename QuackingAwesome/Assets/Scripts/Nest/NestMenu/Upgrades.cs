using Controllers.Duck;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Nest.NestMenu
{
    public class Upgrades : MonoBehaviour
    {
        public Button dashUpgrade;
        public Button beakUpgrade;

        private DucklingsInventory ducklings;
        private DashingBehaviour dashingBehaviour;
        private StickInventory stickInventory;

        public int NeededAmountForDash;
        public int NeededAmountForBeak;

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
            var text = dashUpgrade.transform.Find("CostText").GetComponent<Text>();
            text.text = NeededAmountForDash.ToString();
        }

        public void UpgradeCarryCapacity(int amount)
        {
            stickInventory.maxCapacityOfSticks += amount;
            Debug.Log("New capacity: " + stickInventory.maxCapacityOfSticks);
            ducklings.RemoveDucklings(NeededAmountForBeak);

            NeededAmountForBeak += 1;
            var text = beakUpgrade.transform.Find("CostText").GetComponent<Text>();
            text.text = NeededAmountForBeak.ToString();
        }
    }
}
