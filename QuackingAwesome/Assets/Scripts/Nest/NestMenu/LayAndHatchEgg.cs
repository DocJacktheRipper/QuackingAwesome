using Inventory;
using LeavingScene.Save;
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

            // load number of eggs currently in the nest
            _currentEggs = gameObject.GetComponent<NestBuilding>().nestDataToSave.eggs;
        }

        public int GetNumEggs()
        {
            return _currentEggs;
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

            
        public void HatchEggs()
        {
            // remove energy once for all eggs
            _energyInventory.energy -= neededEnergyForHatchingEgg;
            _ducklings.AddDucklings(_currentEggs);
            
            // increase the millstone "number of eggs hatched"
            GlobalControl.Instance.savedGame.savedMillstonesData.hatchEggs += _currentEggs;
            
            DeactivateAllEggs();

            /* remove energy for every egg
            for(int i=0; i<_currentEggs; i++)
            {
                nest.transform.GetChild(--_currentEggs).gameObject.SetActive(false);
                _energyInventory.energy -= neededEnergyForHatchingEgg;
                _ducklings.AddDucklings(1);
            }*/
            
          }
        
        public bool HasEnoughEnergyForHatching()
        {
            return _energyInventory.energy >= neededEnergyForHatchingEgg;
        }

        private void DeactivateAllEggs()
        {
            while(_currentEggs > 0)
            {
                nest.transform.GetChild(--_currentEggs).gameObject.SetActive(false);
            }
        }
        
        public bool CanHatchEgg()
        {
            return (_currentEggs>0) && HasEnoughEnergyForHatching();
        }
        
        // saving the eggs
        private void OnDestroy()
        {
            gameObject.GetComponent<NestBuilding>().nestDataToSave.eggs = _currentEggs;
        }
    }
}
