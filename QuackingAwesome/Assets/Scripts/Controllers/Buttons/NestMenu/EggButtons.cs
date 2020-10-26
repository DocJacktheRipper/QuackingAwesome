﻿using System;
using Inventory;
using Nest;
using Nest.NestMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Buttons.NestMenu
{
    public class EggButtons : MonoBehaviour
    {
        public LayAndHatchEgg nest;

        public Button layButton;
        public Button hatchButton;
         
        private void Update()
        {
            // enable nestMenu for laying egg
            if (nest.CanLayEgg())
            {
                layButton.interactable = true;
            }
            else
            {
                layButton.interactable = false;
            }
            
            // disable nestMenu for hatching egg
            if (nest.CanHatchEgg())
            {
                hatchButton.interactable = true;
            }
            else
            {
                hatchButton.interactable = false;
            }
        }

        public void LayAnEgg()
        {
            nest.LayEgg();
        }
        
        public void HatchAnEgg()
        {
            nest.HatchEgg();
        }
    }
}
