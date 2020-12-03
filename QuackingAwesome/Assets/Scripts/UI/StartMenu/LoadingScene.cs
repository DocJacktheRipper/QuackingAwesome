using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.LowLevel;

namespace Controllers.Buttons.StartMenu
{
    public class LoadingScene : MonoBehaviour
    {
        public AudioMixer audioMixer;
        
        // To des-activate the menu's buttons in order to keep only the actual background
        public GameObject menuButtons;
        
        // The loading bar to display
        public ProgressBar loadingBar;
        public GameObject loadingScreen;
        AsyncOperation loadingOperation;

        public void LoadNewScene(string sceneName)
        {
            menuButtons.SetActive(false);
            loadingScreen.SetActive(true);
            
            StartFadeOut();
            
            loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        }

        public void StartFadeOut()
        {
            StartCoroutine(FadeMusic.StartFade(audioMixer, "music_volume", 3f, 0));
        }
        
        void Update()
        {
            loadingBar.BarValue = Mathf.Clamp01(loadingOperation.progress / 0.9f) * 100;
        }
    }
}
