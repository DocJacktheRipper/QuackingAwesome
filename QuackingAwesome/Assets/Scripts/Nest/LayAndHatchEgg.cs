﻿using System;
using Inventory;
using UnityEngine;

namespace Nest
{
    public class LayAndHatchEgg : MonoBehaviour
    {
        public GameObject duck;
        
        private EnergyInventory _energyInventory;
        public float neededEnergyForLayingEgg;
        public float neededEnergyForHatchingEgg;

        // private DucklingsInventory _ducklings;

        private int _currentEggs = 0;
        public int maxEggsInNest = 3;

        public void Start()
        {
            _energyInventory = duck.GetComponent<EnergyInventory>();
        }

        public void LayEgg()
        {
            transform.GetChild(_currentEggs++).gameObject.SetActive(true);
            _energyInventory.energy -= neededEnergyForLayingEgg;
        }
        
        public bool HasEnoughEnergyForLaying()
        {
            return _energyInventory.energy >= neededEnergyForLayingEgg;
        }

        public bool CanLayEgg()
        {
            return _currentEggs < maxEggsInNest && HasEnoughEnergyForLaying();
        }

            
        public void HatchEgg()
        {
            transform.GetChild(--_currentEggs).gameObject.SetActive(true);
            _energyInventory.energy -= neededEnergyForHatchingEgg;
            
            // todo: egg inventory update
        }
        
        public bool HasEnoughEnergyForHatching()
        {
            return _energyInventory.energy >= neededEnergyForHatchingEgg;
        }
        
        public bool CanHatchEgg()
        {
            return (_currentEggs>0) && HasEnoughEnergyForHatching();
        }
    }
}
