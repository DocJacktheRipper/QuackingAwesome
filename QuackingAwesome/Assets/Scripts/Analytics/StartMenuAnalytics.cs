using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class StartMenuAnalytics : MonoBehaviour
{
    private Scene _thisScene;
    
    void Awake () {
        _thisScene = SceneManager.GetActiveScene();
        AnalyticsEvent.LevelStart(_thisScene.name, 
            _thisScene.buildIndex);
    }
}
