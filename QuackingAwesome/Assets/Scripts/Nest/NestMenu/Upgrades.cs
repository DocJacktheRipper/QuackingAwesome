using Controllers.Duck;
using Controllers.Duck.Dash;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Nest.NestMenu
{
    public class Upgrades : MonoBehaviour
    {
        public Button dashUpgrade;
        public Button beakUpgrade;

        private DucklingsInventory _ducklings;
        private DashingBehaviour _dashingBehaviour;
        private StickInventory _stickInventory;

        public int NeededAmountForDash;
        public int NeededAmountForBeak;

        private void Start()
        {
            var d = GameObject.Find("Duck");
            _ducklings = d.GetComponent<DucklingsInventory>();
            _stickInventory = d.GetComponent<StickInventory>();

            _dashingBehaviour = d.GetComponent<DashingBehaviour>();
        }

        private void Update()
        {
            // Dash cooldown
            //dashUpgrade.enabled = ducklings.DucklingCount >= NeededAmountForDash;
            dashUpgrade.interactable = _ducklings.DucklingCount >= NeededAmountForDash;

            // Beak capacity
            beakUpgrade.interactable = _ducklings.DucklingCount >= NeededAmountForBeak;
        }


        #region ButtonFunctions

        /******** Button functions ********/
        public void UpgradeDashCooldown(float amount)
        {
            var button = dashUpgrade;
            
            _dashingBehaviour.cooldown -= amount;
            _ducklings.RemoveDucklings(NeededAmountForDash);

            NeededAmountForDash += 1;
            var text = button.transform.Find("CostText").GetComponent<Text>();
            text.text = NeededAmountForDash.ToString();
            
            if (!ToggleVisuals(button.gameObject))
            {
                DisableUpgrade(button);
            }
        }

        public void UpgradeCarryCapacity(int amount)
        {
            var button = beakUpgrade;
            
            _stickInventory.maxCapacityOfSticks += amount;
            Debug.Log("New capacity: " + _stickInventory.maxCapacityOfSticks);
            _ducklings.RemoveDucklings(NeededAmountForBeak);

            NeededAmountForBeak = CalculateAndShowNewCost(NeededAmountForBeak, button);
            if (!ToggleVisuals(button.gameObject))
            {
                DisableUpgrade(button);
            }
        }

        #endregion

        #region HelperMethods

        private void DisableUpgrade(Button upgradeButton)
        {
            var text = upgradeButton.transform.Find("CostText").GetComponent<Text>();
            text.text = "MAX";
            upgradeButton.interactable = false;
            upgradeButton.enabled = false;
        }

        private int CalculateAndShowNewCost(int oldCost, Button upgradeButton)
        {
            oldCost += 1;
            var text = upgradeButton.transform.Find("CostText").GetComponent<Text>();
            text.text = oldCost.ToString();
            return oldCost;
        }

        private bool ToggleVisuals(GameObject button)
        {
            var levelSystem = button.transform.parent.Find("Levels");
            if (levelSystem == null)
                return false; 

            var visualToggle = levelSystem.GetComponent<UpgradeVisualToggle>();
            
            // check if possible
            var isPossible = visualToggle.MoreUpgradesPossible();
            if (isPossible)
            {
                visualToggle.EnableNextStep();
            }

            return isPossible;
        }

        #endregion
    }
}
