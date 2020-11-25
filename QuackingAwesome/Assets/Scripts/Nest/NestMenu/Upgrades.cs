using System;
using Controllers.Duck.Dash;
using Inventory;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

namespace Nest.NestMenu
{
    public class Upgrades : MonoBehaviour
    {
        #region UpgradeProperties

        public Button dashUpgrade;
        public Button beakUpgrade;
        public Button speedUpgrade;
        public Button scareUpgrade;
        public Button nestUpgrade;
                
        private int _neededAmountForDash;
        private int _neededAmountForBeak;
        private int _neededAmountForSpeed;
        private int _neededAmountForScare;
        private int _neededAmountForNest;
                
        private int[] _dashCosts = {2, 4};
        private int _dashStep;
        private int[] _beakCosts = {1, 3};
        private int _beakStep;
        private int[] _speedCosts = {5, 10};
        private int _speedStep;
        private int[] _scareCosts = {3, 5};
        private int _scareStep;
        private int[] _nestCosts = {1, 1};  
        private int _nestStep;      

        #endregion
        

        private DucklingsInventory _ducklings;
        private DashingBehaviour _dashingBehaviour;
        private StickInventory _stickInventory;

        private void Start()
        {
            var d = GameObject.Find("Duck");
            _ducklings = d.GetComponent<DucklingsInventory>();
            _stickInventory = d.GetComponent<StickInventory>();

            _dashingBehaviour = d.GetComponent<DashingBehaviour>();

            InitCostsAndButtons();
        }

        private void InitCostsAndButtons()
        {
            _neededAmountForDash = ShowCost(_dashCosts[_dashStep], dashUpgrade);
            _neededAmountForBeak = ShowCost(_beakCosts[_beakStep], beakUpgrade);
            _neededAmountForSpeed = ShowCost(_speedCosts[_speedStep], speedUpgrade);
            _neededAmountForScare = ShowCost(_scareCosts[_scareStep], scareUpgrade);
            _neededAmountForNest = ShowCost(_nestCosts[_nestStep], nestUpgrade);
        }

        private void Update()
        {
            // enable buttons when enough ducklings
            dashUpgrade.interactable = _ducklings.DucklingCount >= _neededAmountForDash;

            beakUpgrade.interactable = _ducklings.DucklingCount >= _neededAmountForBeak;
            
            speedUpgrade.interactable = _ducklings.DucklingCount >= _neededAmountForSpeed;
            
            scareUpgrade.interactable = _ducklings.DucklingCount >= _neededAmountForScare;
            
            nestUpgrade.interactable = _ducklings.DucklingCount >= _neededAmountForNest;
        }


        #region ButtonFunctions


        public void UpgradeDashCooldown()
        {
            float[] cooldownReduce = {0.5f, 0.5f};

            // remove ducklings from inventory
            _ducklings.RemoveDucklings(_neededAmountForDash);
            
            // make upgrade
            _dashingBehaviour.cooldown -= cooldownReduce[_dashStep];
            
            
            // adjust for next level
            var toggleHandler = GetToggleHandler(dashUpgrade.gameObject);
            if (toggleHandler.MoreUpgradesPossible())
            {
                _neededAmountForDash = int.MaxValue;
                DisableUpgrade(dashUpgrade);
            }
            else
            {
                // increase cost
                if (_dashStep + 1 > _dashCosts.Length)
                    return;
                
                _dashStep++;
                _neededAmountForDash = _dashCosts[_dashStep];
                
                // show visuals
                toggleHandler.EnableNextStep();
            }
        }

        /******** Button functions ********
        public void UpgradeDashCooldown(float amount)
        {
            var button = dashUpgrade;
            
            _dashingBehaviour.cooldown -= amount;
            _ducklings.RemoveDucklings(_neededAmountForDash);

            _neededAmountForDash += 1;
            
            //ShowVisuals(button, _neededAmountForDash);
        }

        public void UpgradeCarryCapacity(int amount)
        {
            var button = beakUpgrade;
            
            _stickInventory.maxCapacityOfSticks += amount;
            Debug.Log("New capacity: " + _stickInventory.maxCapacityOfSticks);
            _ducklings.RemoveDucklings(_neededAmountForBeak++);

            _neededAmountForBeak = ShowCost(_neededAmountForBeak, button);
            if (!ToggleVisuals(button.gameObject))
            {
                DisableUpgrade(button);
            }
        }*/

        #endregion

        #region HelperMethods

        private static void DisableUpgrade(Button upgradeButton)
        {
            var text = upgradeButton.transform.Find("CostText").GetComponent<Text>();
            text.text = "MAX";
            upgradeButton.interactable = false;
            upgradeButton.enabled = false;
        }

        private static int ShowCost(int newCost, Button upgradeButton)
        {
            var text = upgradeButton.transform.Find("CostText").GetComponent<Text>();
            text.text = newCost.ToString();
            return newCost;
        }

        private static UpgradeVisualToggle GetToggleHandler(GameObject button)
        {
            var levelSystem = button.transform.parent.Find("Levels");
            if (levelSystem == null)
                return null; 

            return levelSystem.GetComponent<UpgradeVisualToggle>();
        }
        
        private static bool ToggleVisuals(GameObject button)
        {
            var visualToggle = GetToggleHandler(button);

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
