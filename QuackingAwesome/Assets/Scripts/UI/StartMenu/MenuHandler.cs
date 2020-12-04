using UnityEngine;
using UnityEngine.UI;

namespace UI.StartMenu
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

        private GlobalControl globalControl;

        private void Start()
        {
            TogglePondMapOn(false);
        }

        void Awake()
        {
            GameObject gc = GameObject.Find("GlobalControl");
            globalControl = gc.GetComponent<GlobalControl>();

            CheckLevels();
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

        private void CheckLevels()
        {
            var sceneCompleteID= globalControl.savedPlayerData.higherSceneCompletedID;

            switch (sceneCompleteID)
            {
                case 0:
                    UnlockLevel1(true);
                    UnlockLevel2(false);
                    UnlockLevel3(false);
                    break;
                case 1:    
                    UnlockLevel1(false);
                    UnlockLevel2(true);
                    UnlockLevel3(false);
                    break;
                case 2:    
                    UnlockLevel1(false);
                    UnlockLevel2(false);
                    UnlockLevel3(true);
                    break;
                default:
                    UnlockLevel1(true);
                    UnlockLevel2(true);
                    UnlockLevel3(true);
                    break;
            }
        }

        private void UnlockLevel1(bool unlock)
        {
            level1.interactable = unlock;
        }
        
        private void UnlockLevel2(bool unlock)
        {
            level2.interactable = unlock;
            
            EnableCloudCoverLevel2(!unlock);
        }

        private void UnlockLevel3(bool unlock)
        {
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
