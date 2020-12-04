using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NestAnalytics : MonoBehaviour
{
    private Scene _thisScene;
    void Start()
    {
        _thisScene = SceneManager.GetActiveScene();
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("level", _thisScene);
        UnityEngine.Analytics.Analytics.CustomEvent("NestMenuOpened", customParams);
    }

    public void LayEgg()
    {
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("level", _thisScene);
        UnityEngine.Analytics.Analytics.CustomEvent("LayEgg", customParams);
    }

    public void HashEggs(int eggs)
    {
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("level", _thisScene);
        customParams.Add("eggs", eggs);
        UnityEngine.Analytics.Analytics.CustomEvent("HashEggs", customParams);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
