using Controllers.Buttons.StartMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;

    public GameObject PauseMenuUi;
    public GameObject Controls;
    public LoadingScene loadingScene;

    private void Start()
    {
        
    }

    //Checks input from the menubutton
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
    }


    public void Resume()
    {
        Controls.SetActive(true);
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart()
    {
        Controls.SetActive(true);
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //SceneManager.LoadScene("Tutorial");

        // load current scene
        loadingScene.LoadNewScene(SceneManager.GetActiveScene().name);
    }

    void Pause()
    {
        Controls.SetActive(false);
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void ReturnToStartMenuScene()
    {
        loadingScene.LoadNewScene("StartMenu");
    }
}
