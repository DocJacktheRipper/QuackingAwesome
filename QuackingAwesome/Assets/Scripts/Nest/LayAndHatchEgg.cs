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

        public bool HasEnoughEnergyForLaying()
        {
            return duck.energy >= neededEnergyForLayingEgg;
        }
    
        public void LayEgg()
        {
            transform.GetChild(_currentEggs++).gameObject.SetActive(true);
            duck.energy -= neededEnergyForLayingEgg;
        }

        public bool HasEnoughEnergyForHatching()
        {
            return duck.energy >= neededEnergyForHatchingEgg;
        }
        
        public void HatchEgg()
        {
            transform.GetChild(--_currentEggs).gameObject.SetActive(true);
            duck.energy -= neededEnergyForHatchingEgg;
        }
    }
}
