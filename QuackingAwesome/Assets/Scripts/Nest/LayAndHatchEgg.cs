using Inventory;
using UnityEngine;

namespace Nest
{
    public class LayAndHatchEgg : MonoBehaviour
    {
        public EnergyInventory duck;
        public float neededEnergyForLayingEgg;
        public float neededEnergyForHatchingEgg;

        private int _currentEggs = 0;
        public int maxEggsInNest = 3;

        public void LayEgg()
        {
            transform.GetChild(_currentEggs++).gameObject.SetActive(true);
            duck.energy -= neededEnergyForLayingEgg;
        }
        
        public bool HasEnoughEnergyForLaying()
        {
            return duck.energy >= neededEnergyForLayingEgg;
        }

        public bool CanLayEgg()
        {
            return _currentEggs < maxEggsInNest && HasEnoughEnergyForLaying();
        }

            
        public void HatchEgg()
        {
            transform.GetChild(--_currentEggs).gameObject.SetActive(true);
            duck.energy -= neededEnergyForHatchingEgg;
            
            // todo: egg inventory update
        }
        
        public bool HasEnoughEnergyForHatching()
        {
            return duck.energy >= neededEnergyForHatchingEgg;
        }
        
        public bool CanHatchEgg()
        {
            return (_currentEggs>0) && HasEnoughEnergyForHatching();
        }
    }
}
