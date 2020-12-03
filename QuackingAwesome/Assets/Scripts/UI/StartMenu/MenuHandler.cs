using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Buttons.StartMenu
{
    public class MenuHandler : MonoBehaviour
    {
        public GameObject titleMenu;
        public GameObject pondMap;

        public Button level1;
        public Button level2;
        public Button level3;
        
        public GameObject cloudCoverLevel2;
        public GameObject cloudCoverLevel3;

        private void Start()
        {
            TogglePondMapOn(false);
        }

        private void TogglePondMapOn(bool openPond)
        {
            titleMenu.SetActive(!openPond);
            pondMap.SetActive(openPond);
        }

        public void OpenPondMap()
        {
            TogglePondMapOn(true);
        }

        #region PondMap

        public void UnlockLevel2(bool unlock)
        {
            level1.interactable = !unlock;
            level2.interactable = unlock;
            
            EnableCloudCoverLevel2(!unlock);
        }
        
        public void UnlockLevel3(bool unlock)
        {
            level2.interactable = !unlock;
            level3.interactable = unlock;
            
            EnableCloudCoverLevel3(!unlock);
        }

        private void EnableCloudCoverLevel2(bool cloudEnable)
        {
            cloudCoverLevel2.SetActive(cloudEnable);
        }

        private void EnableCloudCoverLevel3(bool cloudEnable)
        {
            cloudCoverLevel3.SetActive(cloudEnable);
        }

        #endregion
    }
}
