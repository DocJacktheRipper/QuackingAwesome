using Nest;
using Props;
using UnityEngine;

namespace Controllers.Buttons.NestMenu
{
    public class NestButton : MonoBehaviour
    {
        public NestBuilding nest;

        public GameObject nestButton;
        public GameObject nestMenuUI;

        public GameObject Controls;


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
            if (nestMenuUI.activeSelf)
            {
                // disable menu [nest]
                nestMenuUI.SetActive(false);
                // enable joystick
                Controls.SetActive(true);
            }
            else
            {
                // enable menu [nest]
                nestMenuUI.SetActive(true);
                // disable joystick
                Controls.SetActive(false);
            }
        }

        public void CloseNestMenu()
        {
            nestMenuUI.SetActive(false);
            Controls.SetActive(true);
        }
    }
}
