using Nest;
using Props;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Controllers.Buttons.NestMenu
{
    public class NestButton : MonoBehaviour
    {
        public NestBuilding nest;

        public GameObject nestButton;
        public GameObject nestMenuUI;
        public GameObject mainMenuButton;

        public GameObject controls;


        public void ActivateNestButton()
        {
            if(nest.NestIsFinished)
                nestButton.SetActive(true);
        }

        public void DeactivateNestButton()
        {
            nestButton.SetActive(false);
        }

        public void ExpandNestMenu()
        {
            // enable menu [nest]
                nestMenuUI.SetActive(true);
                // disable nest button
                nestButton.SetActive(false);
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
            // enable main menu button
            mainMenuButton.SetActive(true);
            // enable joystick
            controls.SetActive(true);
        }
    }
}
