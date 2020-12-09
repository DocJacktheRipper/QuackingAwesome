using System.Collections.Generic;
using Controllers.Duck;
using Controllers.Duck.Dash;
using Controllers.Duck.Quack;
using Inventory;
using LeavingScene.Save;
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

        private readonly int[]   _dashCosts      = {2, 4};
        private readonly float[] _cooldownReduce = {0.5f, 0.5f};
        private          int     _dashStep       = 0;
        
        private readonly int[]   _beakCosts       = {1, 3};
        private readonly int[]   _newBeakCapacity = {2, 3};
        private          int     _beakStep        = 0;
        
        private readonly int[]   _speedCosts      = {5, 10};
        private readonly float[] _speedMultiplier = {1.2f, 1.5f};
        private          int     _speedStep       = 0;
        
        private readonly int[]   _scareCosts      = {3, 5};
        private readonly float[] _scareChance     = {2, 3};
        private          int     _scareStep       = 0;
        
        private readonly int[]   _nestCosts       = {1, 1};
        private readonly int[]   _addNestCapacity = {1, 1};
        private          int     _nestStep        = 0;

        #endregion
        
        private DucklingsInventory _ducklings;
        private StickInventory _stickInventory;
        private DashingBehaviour _dashingBehaviour;
        private QuackingArea _quackingArea;
        private CharacterControl _characterControl;

        // applied ba MultipleNestHandler
        public Transform nestsParent;
        private List<LayAndHatchEgg> _layAndHatchEggList;

        private UpgradesProgression _globalControl;

        private void Start()
        {
            var d = GameObject.Find("Duck");
            _ducklings = d.GetComponent<DucklingsInventory>();
            _stickInventory = d.GetComponent<StickInventory>();

            _characterControl = d.GetComponent<CharacterControl>();
            _dashingBehaviour = d.GetComponent<DashingBehaviour>();
            _quackingArea = d.transform.Find("QuackingCone").GetComponent<QuackingArea>();

            var nestContainer = GameObject.Find("DuckNests");
            if (nestContainer != null)
            {
                nestsParent = nestContainer.transform;
            }

            _layAndHatchEggList = new List<LayAndHatchEgg>();
            for (int i = 0; i < nestsParent.childCount; i++)
            {
                _layAndHatchEggList.Add(nestsParent.GetChild(i).GetComponent<LayAndHatchEgg>());
            }


            _globalControl = GlobalControl.Instance.savedGame.savedUpgradesProgression;

            InitCostsAndButtons();
        }

        private void InitCostsAndButtons()
        {
            InitUpgradeDashCooldown(_globalControl.dashStep);
            _neededAmountForDash  = ShowCost(_dashCosts[_dashStep], dashUpgrade);
            
            InitUpgradeBeakCapacity(_globalControl.beakStep);
            _neededAmountForBeak  = ShowCost(_beakCosts[_beakStep], beakUpgrade);
            
            InitUpgradeDuckSpeed(_globalControl.speedStep);
            _neededAmountForSpeed = ShowCost(_speedCosts[_speedStep], speedUpgrade);
            
            InitUpgradeScareChance(_globalControl.scareStep);
            _neededAmountForScare = ShowCost(_scareCosts[_scareStep], scareUpgrade);
            
            InitUpgradeNestCapacity(_globalControl.nestStep);
            _neededAmountForNest  = ShowCost(_nestCosts[_nestStep], nestUpgrade);
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

        #region Dash

        private void ChangeDashCooldown()
        {
            // make upgrade
            _dashingBehaviour.cooldown -= _cooldownReduce[_dashStep];
            
            // increase cost
            if (_dashStep + 1 < _dashCosts.Length)
                _dashStep++;

            _neededAmountForDash = _dashCosts[_dashStep];
            ShowCost(_neededAmountForDash, dashUpgrade);

            // adjust toggle
            if (!MoreUpgradesPossible(dashUpgrade))
                _neededAmountForDash = int.MaxValue;
        }
        private void InitUpgradeDashCooldown(int step)
        {
            for (int i = 0; i < step; i++)
                ChangeDashCooldown();
        }

        public void UpgradeDashCooldown()
        {
            // remove ducklings from inventory
            _ducklings.RemoveDucklings(_neededAmountForDash);
            
            ChangeDashCooldown();
        }

        #endregion

        #region BeakCapacity
        private void ChangeBeakCapacity()
        {
            // make upgrade
            _stickInventory.maxCapacityOfSticks = _newBeakCapacity[_beakStep];
            
            // increase cost
            if (_beakStep + 1 < _beakCosts.Length) 
                _beakStep++;
            
            _neededAmountForBeak = _beakCosts[_beakStep];
            ShowCost(_neededAmountForBeak, beakUpgrade);
            
            // adjust toggle
            if (!MoreUpgradesPossible(beakUpgrade))  
                _neededAmountForBeak = int.MaxValue;
        }
        
        private void InitUpgradeBeakCapacity(int step)
        {
            for (int i = 0; i < step; i++)
                ChangeBeakCapacity();
        }

        public void UpgradeBeakCapacity()
        {
            // remove ducklings from inventory
            _ducklings.RemoveDucklings(_neededAmountForBeak);
            
            ChangeBeakCapacity();

        }
        

        #endregion

        #region Speed

        private void ChangeDuckSpeed()
        {
            // make upgrade
            _characterControl.AddSpeedModifier(_speedMultiplier[_speedStep]);
            
            // increase cost
            if (_speedStep + 1 < _speedCosts.Length)
                _speedStep++;
            
            _neededAmountForSpeed = _speedCosts[_speedStep];
            ShowCost(_neededAmountForSpeed, speedUpgrade);

            // adjust toggle
            if (!MoreUpgradesPossible(speedUpgrade))
                _neededAmountForSpeed = int.MaxValue;
        }
        
        private void InitUpgradeDuckSpeed(int step)
        {
            for (int i = 0; i < step; i++)
                ChangeDuckSpeed();
        }
        public void UpgradeDuckSpeed()
        {
            // remove ducklings from inventory
            _ducklings.RemoveDucklings(_neededAmountForSpeed);
            
            ChangeDuckSpeed();
        }
        

        #endregion

        #region ScareChance

        private void ChangeScareChance()
        {
            // make upgrade
            _quackingArea.SetVolumeMultiplier(_scareChance[_scareStep]);
            
            // increase cost
            if (_scareStep + 1 < _scareCosts.Length)
                _scareStep++;
            
            _neededAmountForScare = _scareCosts[_scareStep];
            ShowCost(_neededAmountForScare, scareUpgrade);
            
            // adjust toggle
            if (!MoreUpgradesPossible(scareUpgrade))
                _neededAmountForScare = int.MaxValue;
        }
        private void InitUpgradeScareChance(int step)
        {
            for (int i = 0; i < step; i++)
                ChangeScareChance();
        }
        public void UpgradeScareChance()
        {
            // remove ducklings from inventory
            _ducklings.RemoveDucklings(_neededAmountForScare);
            
            ChangeScareChance();
        }
        #endregion

        #region NestCapacity

        private void ChangeNestCapacity()
        {
            // make upgrade
            foreach (var nest in _layAndHatchEggList)
            {
                Debug.Log("Nest: " + nest.name);
                var temp = _addNestCapacity[_nestStep];
                nest.maxEggsInNest += temp;
            }
            
            // increase cost
            if (_nestStep + 1 < _nestCosts.Length)
                _nestStep++;
            
            _neededAmountForNest = _nestCosts[_nestStep];
            ShowCost(_neededAmountForNest, nestUpgrade);
            
            // adjust toggle
            if (!MoreUpgradesPossible(nestUpgrade))
                _neededAmountForNest = int.MaxValue;
        }
        private void InitUpgradeNestCapacity(int step)
        {
            for (int i = 0; i < step; i++)
                ChangeNestCapacity();
        }
        public void UpgradeNestCapacity()
        {
            // remove ducklings from inventory
            _ducklings.RemoveDucklings(_neededAmountForNest);

            ChangeNestCapacity();
        }
        
        #endregion
        
        
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
            _globalControl.dashStep  = _dashStep;
            _globalControl.beakStep  = _beakStep;
            _globalControl.speedStep = _speedStep;
            _globalControl.scareStep = _scareStep;
            _globalControl.nestStep  = _nestStep;
        }
    }
}
