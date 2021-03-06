﻿using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class PeaBar : MonoBehaviour
    {
        public GameObject duck;
        private EnergyInventory _inventory;
        private Text _text;

        private int _energy;
    
        void Start()
        {
            duck = GameObject.Find("Duck");
            _inventory = duck.GetComponent<EnergyInventory>();
            _text = GetComponent<Text>();
            _energy = (int) _inventory.energy;
            _text.text = _energy.ToString();
        }

        void Update()
        {
            if (_energy != (int) _inventory.energy)
            {
                _energy = (int) _inventory.energy;
                _text.text = _energy.ToString();
            }
        }
    }
}
