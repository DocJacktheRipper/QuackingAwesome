using LeavingScene.Save;
using UnityEngine;
using UnityEngine.UI;

namespace UI.StartMenu
{
    public class MenuHandler : MonoBehaviour
    {
        public GameObject titleMenu;
        private GameObject _credits;
        private GameObject _menuButtons;
        public GameObject pondMap;

        public Button level1;
        public Button level2;
        public Button level3;
        
        public GameObject cloudCoverLevel2;
        public GameObject cloudCoverLevel3;

        private GlobalControl _globalControl;
        
        // animation
        private CloudAnimationEvent _cloud2Animator;
        private CloudAnimationEvent _cloud3Animator;

        private void Start()
        {
            var gc = GameObject.Find("GlobalControl");
            _globalControl = gc.GetComponent<GlobalControl>();
            
            TogglePondMapOn(false);
            _credits = titleMenu.transform.Find("CreditsPanel").gameObject;
            _menuButtons = titleMenu.transform.Find("MenuButtons").gameObject;
            
            _cloud2Animator = cloudCoverLevel2.GetComponent<CloudAnimationEvent>();
            _cloud3Animator = cloudCoverLevel3.GetComponent<CloudAnimationEvent>();
        }

        public void OpenCreditScreen()
        {
            _credits.SetActive(true);
            _menuButtons.SetActive(false);
        }

        public void CloseCreditScreen()
        {
            _credits.SetActive(false);
            _menuButtons.SetActive(true);
        }

        private void TogglePondMapOn(bool openPond)
        {
            titleMenu.SetActive(!openPond);
            pondMap.SetActive(openPond);
        }

        public void OpenPondMap()
        {
            TogglePondMapOn(true);
            
            CheckLevels();
        }
        
        public void ClosePondMap()
        {
            TogglePondMapOn(false);
        }
        

        #region PondMap

        public void CheckLevels()
        {
            var sceneCompleteID= _globalControl.savedGame.higherSceneCompletedID;

            if (sceneCompleteID + 1 < _globalControl.savedGame.savedScenes.Length)
            {
                var sceneData = _globalControl.savedGame.savedScenes[sceneCompleteID + 1];
                if (sceneData.saveTasksProgression.tasksAreCompleted)
                {
                    sceneCompleteID = sceneCompleteID + 1;
                    _globalControl.savedGame.higherSceneCompletedID = sceneCompleteID;
                }
            }
            
            switch (sceneCompleteID)
            {
                case 0:
                    Debug.Log("Map 1 is revealed.");
                    OnlyLevel1();
                    break;
                case 1:   
                    Debug.Log("Map 2 is revealed.");
                    OnlyLevel2();
                    break;
                case 2:  
                    Debug.Log("Map 3 is revealed.");
                    OnlyLevel3();
                    break;
                default:
                    Debug.Log("All Maps are revealed.");
                    AllLevelUnlocked();
                    break;
            }
        }
        //********* For now, without animation **********//
        /*
        public void CheckLevels()
        {
            var sceneCompleteID= _globalControl.savedGame.higherSceneCompletedID;

            SceneData sceneData;
            switch (sceneCompleteID)
            {
                case 0:
                    Debug.Log("Map 1 is revealed.");
                    OnlyLevel1();

                    sceneData = _globalControl.savedGame.savedScenes[sceneCompleteID + 1];
                    if (sceneData.saveTasksProgression.tasksAreCompleted)
                    {
                        _globalControl.savedGame.higherSceneCompletedID = sceneCompleteID+1;
                        UnlockLevel1(false);
                        // invoke animation ("Completed" over lv1?)
                        RevealCloudsLv2();
                        Invoke(nameof(OnlyLevel2), 0.2f);
                    }
                    break;
                case 1:   
                    Debug.Log("Map 2 is revealed.");
                    OnlyLevel2();
                    
                    sceneData = _globalControl.savedGame.savedScenes[sceneCompleteID + 1];
                    if (sceneData.saveTasksProgression.tasksAreCompleted)
                    {
                        _globalControl.savedGame.higherSceneCompletedID = sceneCompleteID+1;
                        Debug.Log("Map 3 is newly revealed.");
                        OnlyLevel3();
                        RevealCloudsLv3();
                    }
                    break;
                case 2:  
                    Debug.Log("Map 3 is revealed.");
                    OnlyLevel3();
                    break;
                default:
                    Debug.Log("All Maps are revealed.");
                    AllLevelUnlocked();
                    break;
            }
        }*/

        #region OnlyOneLevelUnlocked

        private void OnlyLevel1()
        {
            UnlockLevel1(true);
            UnlockLevel2(false);
            UnlockLevel3(false);
        }

        private void OnlyLevel2()
        {
            UnlockLevel1(false);
            UnlockLevel2(true);
            UnlockLevel3(false);
            EnableCloudCoverLevel2(false);
        }

        private void OnlyLevel3()
        {
            UnlockLevel1(false);
            UnlockLevel2(false);
            UnlockLevel3(true);
            EnableCloudCoverLevel2(false);
            EnableCloudCoverLevel3(false);
        }

        private void AllLevelUnlocked()
        {
            UnlockLevel1(true);
            UnlockLevel2(true);
            UnlockLevel3(true);
        }

        #endregion
        #region NoAnimation

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
        #region AnimatedReveal

        private void RevealCloudsLv2()
        {
            _cloud2Animator.InvokeAnimation();
        }

        private void RevealCloudsLv3()
        {
            _cloud3Animator.InvokeAnimation();
        }

        #endregion

        #endregion
        
    }
}
