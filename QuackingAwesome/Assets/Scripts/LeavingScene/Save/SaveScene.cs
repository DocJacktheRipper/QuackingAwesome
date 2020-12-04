using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScene : MonoBehaviour
{
    void OnDestroy()
    {
        PlayerData globalControl = GlobalControl.Instance.savedPlayerData;
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        // if (globalControl.higherSceneCompletedID < sceneID ) globalControl.higherSceneCompletedID = sceneID;
        globalControl.currentScene.id = sceneID;
    }

}
