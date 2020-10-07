using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Buttons.StartMenu
{
    public class LoadScene : MonoBehaviour
    {
        public string sceneName;

        public void LoadNewScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
