using System;
using UnityEngine;

namespace UI.Main_Menu.PauseMenu
{
    public class PauseMenuHandler : MonoBehaviour
    {
        public GameObject settings;
        public GameObject milestones;
        public GameObject tasks;

        private void Start()
        {
            settings.SetActive(true);
            milestones.SetActive(false);
            tasks.SetActive(false);
        }

        public void SettingClick()
        {
            settings.SetActive(true);
            milestones.SetActive(false);
            tasks.SetActive(false);
        }
        
        public void MilestoneClick()
        {
            settings.SetActive(false);
            milestones.SetActive(true);
            tasks.SetActive(false);
        }

        public void TaskClick()
        {
            settings.SetActive(false);
            milestones.SetActive(false);
            tasks.SetActive(true);
        }
    }
}
