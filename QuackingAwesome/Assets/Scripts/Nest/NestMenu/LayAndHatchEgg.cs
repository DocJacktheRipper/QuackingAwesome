using Inventory;
using UnityEngine;

namespace Nest.NestMenu
{
    public class LayAndHatchEgg : MonoBehaviour
    {
        public GameObject nest;
        
        private GameObject duck;
        
        private EnergyInventory _energyInventory;
        public float neededEnergyForLayingEgg;
        public float neededEnergyForHatchingEgg;

        private DucklingsInventory _ducklings;

        private int _currentEggs = 0;
        public int maxEggsInNest = 3;

        public void Start()
        {
            duck = GameObject.FindWithTag("Player");
            _energyInventory = duck.GetComponent<EnergyInventory>();
            _ducklings = duck.GetComponent<DucklingsInventory>();
        }

        public void LayEgg()
        {
            nest.transform.GetChild(_currentEggs++).gameObject.SetActive(true);
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
            for(int i=0; i<_currentEggs; i++)
            {
                nest.transform.GetChild(--_currentEggs).gameObject.SetActive(false);
                _energyInventory.energy -= neededEnergyForHatchingEgg;
                _ducklings.AddDucklings(1);
            }
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
