using System;
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
        
        private NestAnalytics _analytics;

        private void Start()
        {
            _analytics = GameObject.Find("NestAnalytics").GetComponent<NestAnalytics>();
        }

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
            _analytics.LayEgg();
        }
        
        public void HatchAnEgg()
        {
            nest.HatchEggs();
            _analytics.HashEggs(nest.GetNumEggs());
        }
    }
}
