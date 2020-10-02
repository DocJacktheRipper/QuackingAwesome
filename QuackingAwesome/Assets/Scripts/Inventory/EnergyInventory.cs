﻿using UnityEngine;

namespace Inventory
{
    public class EnergyInventory : MonoBehaviour
    {
        public float energy;

        private void Start()
        {
            energy = 0;
        }

        public void IncreaseEnergy(int value)
        {
            energy += value;
        }
    }
}
