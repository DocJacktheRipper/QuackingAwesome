using Inventory.UI;
using Nest;
using Props;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Controllers.Buttons.NestMenu
{
    public class NestButton : MonoBehaviour
    {
        public NestBuilding nest;

        public GameObject controls;
        public GameObject mainMenuButton;
        public GameObject nestButton;
        public GameObject nestMenuUI;
        public GameObject statistics;
        
        public StickBar stickBar;
  
        public void ActivateNestButton()
        {
            if (nest.NestIsFinished) nestButton.SetActive(true);
            else stickBar.DisplayStickStatistics(true);
        }

        public void DeactivateNestButton()
        {
            nestButton.SetActive(false);
            if (!stickBar.stayVisible) stickBar.DisplayStickStatistics(false);
        }

        public void ExpandNestMenu()
        {       
            // pause the game
            Time.timeScale = 0;
            // enable menu [nest]
            nestMenuUI.SetActive(true);
            // disable nest button
            nestButton.SetActive(false);
            // disable statistics
            statistics.SetActive(false);
            // disable main menu button
            mainMenuButton.SetActive(false);
            // disable joystick
            controls.SetActive(false);
        }

        public void CloseNestMenu()
        {
            // disable menu [nest]
            nestMenuUI.SetActive(false);
            // enable nest button
            nestButton.SetActive(true);
            // enable statistics
            statistics.SetActive(true);
            // enable main menu button
            mainMenuButton.SetActive(true);
            // enable joystick
            controls.SetActive(true);
            // restart the game
            Time.timeScale = 1;
        }
    }
}
