using System;
using UnityEngine;

namespace Inventory
{
    public class EnergyInventory : MonoBehaviour
    {
        public float energy;

        private void Start()
        {
            // load the player energy
            energy = GlobalControl.Instance.savedPlayerData.savedInventoryData.energy;
        }

        public void IncreaseEnergy(int value)
        {
            energy += value;
        }

        // saving the player energry
        private void OnDestroy()
        {
            GlobalControl.Instance.savedPlayerData.savedInventoryData.energy = energy;
        }
    }
}
