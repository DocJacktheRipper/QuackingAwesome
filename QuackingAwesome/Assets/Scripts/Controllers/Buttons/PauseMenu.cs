using Controllers.Buttons.StartMenu;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Controllers.Buttons
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused;

        public GameObject pauseMenuUi;
        public GameObject statistics;
        public GameObject nestMenu;
        public GameObject controls;
        public LoadingScene loadingScene;
        public AudioMixer audio;

        public void Start()
        {
            pauseMenuUi = transform.Find("PauseMenu").gameObject;

            GameIsPaused = true;
            ToggleMainMenu();
        }

        public void ToggleMainMenu()
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        private void EnableOtherUI(bool enable)
        {
            controls.SetActive(enable);
            nestMenu.SetActive(enable);
            statistics.SetActive(enable);
        }
    
        void Pause()
        {
            EnableOtherUI(false);
            pauseMenuUi.SetActive(true);
            Time.timeScale = 0;
            audio.SetFloat("sound_volume", 0);
        
            GameIsPaused = true;
        }
    
        public void Resume()
        {
            EnableOtherUI(true);
            pauseMenuUi.SetActive(false);
            Time.timeScale = 1f;
            audio.SetFloat("sound_volume", 0);
        
            GameIsPaused = false;
        }

        public void Restart()
        {
            Resume();

            // load current scene
            loadingScene.LoadNewScene(SceneManager.GetActiveScene().name);
        }
        public void ReturnToStartMenuScene()
        {
            loadingScene.LoadNewScene("StartMenu");
        }
    }
}
