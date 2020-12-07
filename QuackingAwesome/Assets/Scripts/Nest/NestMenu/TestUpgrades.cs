using System;
using Controllers.Duck;
using Controllers.Duck.Dash;
using Controllers.Duck.Quack;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Nest.NestMenu
{
    public class TestUpgrades : MonoBehaviour
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
                
        private readonly int[] _dashCosts = {2, 4};
        private int _dashStep = 0;
        private readonly int[] _beakCosts = {1, 3};
        private int _beakStep = 0;
        private readonly int[] _speedCosts = {5, 10};
        private int _speedStep = 0;
        private readonly int[] _scareCosts = {3, 5};
        private int _scareStep = 0;
        private readonly int[] _nestCosts = {1, 1};  
        private int _nestStep = 0;      

        #endregion
        

        private DucklingsInventory _ducklings;
        private StickInventory _stickInventory;
        private DashingBehaviour _dashingBehaviour;
        private QuackingArea _quackingArea;
        private CharacterControl _characterControl;

        public GameObject nest;
        private LayAndHatchEgg _layAndHatchEgg;

        private UpgradesProgression _savedUpgradesProgression;

        private void Start()
        {
            var d = GameObject.Find("Duck");
            _ducklings = d.GetComponent<DucklingsInventory>();
            _stickInventory = d.GetComponent<StickInventory>();

            _characterControl = d.GetComponent<CharacterControl>();
            _dashingBehaviour = d.GetComponent<DashingBehaviour>();
            _quackingArea = d.transform.Find("QuackingCone").GetComponent<QuackingArea>();

            _layAndHatchEgg = nest.GetComponent<LayAndHatchEgg>();

            _savedUpgradesProgression = GlobalControl.Instance.savedGame.savedUpgradesProgression;

            InitCostsAndButtons();
        }

        private void InitCostsAndButtons()
        {
            UpgradeDashCooldown(_savedUpgradesProgression.dashStep, true);
            UpgradeBeakCapacity(_savedUpgradesProgression.beakStep, true);
            UpgradeDuckSpeed(_savedUpgradesProgression.speedStep, true);
            UpgradeScareChance(_savedUpgradesProgression.scareStep, true);
            UpgradeNestCapacity(_savedUpgradesProgression.nestStep, true);

        }

        private void Update()
        {
            // enable buttons when enough ducklings
            dashUpgrade.interactable = _ducklings.ducklingCount >= _neededAmountForDash;

            beakUpgrade.interactable = _ducklings.ducklingCount >= _neededAmountForBeak;
            
            speedUpgrade.interactable = _ducklings.ducklingCount >= _neededAmountForSpeed;
            
            scareUpgrade.interactable = _ducklings.ducklingCount >= _neededAmountForScare;
            
            nestUpgrade.interactable = _ducklings.ducklingCount >= _neededAmountForNest;
        }


        #region ButtonFunctions


        public void UpgradeDashCooldown(int step = 1, bool initialization = false)
        {
            float[] cooldownReduce = {0.5f, 0.5f};

            if (!initialization)
            {
                // remove ducklings from inventory
                _ducklings.RemoveDucklings(_neededAmountForDash);
            }

            Debug.Log("step " + step);
            Debug.Log("_dashStep " + _dashStep);
            for (int i = 0; i < step; i++)
            {
                Debug.Log("in for i, step = "+ step);
                // make upgrade
                _dashingBehaviour.cooldown -= cooldownReduce[_dashStep];
            
                // increase cost
                if (_dashStep + 1 < _dashCosts.Length)
                {
                    _dashStep++;
                }
                
                // adjust toggle
                if (!MoreUpgradesPossible(dashUpgrade))
                {
                    _neededAmountForDash = int.MaxValue;
                }
            }

            _neededAmountForDash = _dashCosts[_dashStep];
            ShowCost(_neededAmountForDash, dashUpgrade);
        }
        
        public void UpgradeBeakCapacity(int step = 1, bool initialization = false)
        {
            int[] newCapacity = {2, 3};

            if (!initialization)
            {
                // remove ducklings from inventory
                _ducklings.RemoveDucklings(_neededAmountForBeak);
            }

            for (int i = 0; i < step; i++)
            {
                // make upgrade
                _stickInventory.maxCapacityOfSticks = newCapacity[_beakStep];

                // increase cost
                if (_beakStep + 1 < _beakCosts.Length)
                {
                    _beakStep++;
                }
            }

            _neededAmountForBeak = _beakCosts[_beakStep];
            ShowCost(_neededAmountForBeak, beakUpgrade);
            
            
            // adjust toggle
            if (!MoreUpgradesPossible(beakUpgrade))
            {
                _neededAmountForBeak = int.MaxValue;
            }
        }
        
        public void UpgradeDuckSpeed(int step = 1, bool initialization = false)
        {
            float[] multiplier = {1.2f, 1.5f};

            if (!initialization)
            {
                // remove ducklings from inventory
                _ducklings.RemoveDucklings(_neededAmountForSpeed);
            }

            for (int i = 0; i < step; i++)
            {
                // make upgrade
                _characterControl.AddSpeedModifier(multiplier[_speedStep]);

                // increase cost
                if (_speedStep + 1 < _speedCosts.Length)
                {
                    _speedStep++;
                }
            }

            _neededAmountForSpeed = _speedCosts[_speedStep];
            ShowCost(_neededAmountForSpeed, speedUpgrade);
            
            
            // adjust toggle
            if (!MoreUpgradesPossible(speedUpgrade))
            {
                _neededAmountForSpeed = int.MaxValue;
            }
        }
        
        public void UpgradeScareChance(int step = 1, bool initialization = false)
        {
            float[] scareChance = {2, 3};

            if (!initialization)
            {
                // remove ducklings from inventory
                _ducklings.RemoveDucklings(_neededAmountForScare);
            }
            

            for (int i = 0; i < step; i++)
            {
                // make upgrade
                _quackingArea.SetVolumeMultiplier(scareChance[_scareStep]);

                // increase cost
                if (_scareStep + 1 < _scareCosts.Length)
                {
                    _scareStep++;
                }
            }

            _neededAmountForScare = _scareCosts[_scareStep];
            ShowCost(_neededAmountForScare, scareUpgrade);
            
            
            // adjust toggle
            if (!MoreUpgradesPossible(scareUpgrade))
            {
                _neededAmountForScare = int.MaxValue;
            }
        }
        
        public void UpgradeNestCapacity(int step = 1, bool initialization = false)
        {
            int[] addCapacity = {1, 1};

            if (!initialization)
            {
                // remove ducklings from inventory
                _ducklings.RemoveDucklings(_neededAmountForNest);
            }

            for (int i = 0; i < step; i++)
            {
                // make upgrade
                _layAndHatchEgg.maxEggsInNest += addCapacity[_nestStep];

                // increase cost
                if (_nestStep + 1 < _nestCosts.Length)
                {
                    _nestStep++;
                }
            }

            _neededAmountForNest = _nestCosts[_nestStep];
            ShowCost(_neededAmountForNest, nestUpgrade);
            
            
            // adjust toggle
            if (!MoreUpgradesPossible(nestUpgrade))
            {
                _neededAmountForNest = int.MaxValue;
            }
        }
        
        #endregion

        #region HelperMethods

        private static void DisableUpgrade(Button upgradeButton)
        {
            var text = upgradeButton.transform.Find("CostText").GetComponent<Text>();
            text.text = "MAX";
            upgradeButton.interactable = false;
            //upgradeButton.enabled = false;
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

        private static bool MoreUpgradesPossible(Button button)
        {
            var toggleHandler = GetToggleHandler(button.gameObject);
            if (toggleHandler.MoreUpgradesPossible())
            {
                // show visuals
                toggleHandler.EnableNextStep();
            }
            
            if(!toggleHandler.MoreUpgradesPossible())
            {
                DisableUpgrade(button);
                return false;
            }

            return true;
        }

        #endregion

        private void OnDestroy()
        {
            _savedUpgradesProgression.dashStep  = _dashStep;
            _savedUpgradesProgression.beakStep  = _beakStep;
            _savedUpgradesProgression.speedStep = _speedStep;
            _savedUpgradesProgression.scareStep = _scareStep;
            _savedUpgradesProgression.nestStep  = _nestStep;
        }
    }
}
