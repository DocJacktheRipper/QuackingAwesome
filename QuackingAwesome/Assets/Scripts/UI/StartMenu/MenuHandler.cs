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

        private GlobalControl _globalControl;
        
        // animation
        private CloudAnimationEvent _cloud2Animator;
        private CloudAnimationEvent _cloud3Animator;

        private void Start()
        {
            TogglePondMapOn(false);
            _cloud2Animator = cloudCoverLevel2.GetComponent<CloudAnimationEvent>();
            _cloud3Animator = cloudCoverLevel3.GetComponent<CloudAnimationEvent>();
        }

        void Awake()
        {
            GameObject gc = GameObject.Find("GlobalControl");
            _globalControl = gc.GetComponent<GlobalControl>();
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

        #region PondMap

        public void CheckLevels()
        {
            var sceneCompleteID= _globalControl.savedPlayerData.higherSceneCompletedID;

            SceneData scene;
            switch (sceneCompleteID)
            {
                case 0:
                    OnlyLevel1();
                    
                    scene = _globalControl.savedPlayerData.currentScene;
                    if (scene.id == 1 && scene.saveTasksProgression.tasksAreCompleted)
                    {
                        _globalControl.savedPlayerData.higherSceneCompletedID = scene.id;
                        UnlockLevel1(false);
                        // invoke animation ("Completed" over lv1?)
                        RevealCloudsLv2();
                    }
                    break;
                case 1:   
                    OnlyLevel2();
                    
                    scene = _globalControl.savedPlayerData.currentScene;
                    if (scene.id == 2 && scene.saveTasksProgression.tasksAreCompleted)
                    {
                        _globalControl.savedPlayerData.higherSceneCompletedID = scene.id;
                        UnlockLevel2(false);
                        RevealCloudsLv3();
                    }
                    break;
                case 2:    
                    OnlyLevel3();
                    break;
                default:
                    AllLevelUnlocked();
                    break;
            }
        }

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
        }

        private void OnlyLevel3()
        {
            UnlockLevel1(false);
            UnlockLevel2(false);
            UnlockLevel3(true);
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
