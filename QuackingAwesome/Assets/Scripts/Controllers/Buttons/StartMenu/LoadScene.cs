using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Controllers.Buttons.StartMenu
{
    public class LoadScene : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public string sceneName;

        public void LoadNewScene()
        {
            StartFadeOut();
            SceneManager.LoadScene(sceneName);
        }
        
        public void StartFadeOut()
        {
            StartCoroutine(FadeMusic.StartFade(audioMixer, "music_fade", 3f, 0));
        }
    }
}
