#define VERBOSE

using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScene : MonoBehaviour
{
    private GameData _globalControl;
    private int _sceneID;
    
    private void Awake()
    {
        _sceneID = SceneManager.GetActiveScene().buildIndex;
        _globalControl = GlobalControl.Instance.savedGame;
#if VERBOSE
        Debug.Log("Scene index " + _sceneID);   
#endif
        if (_globalControl.savedScenes[_sceneID] == null)
        {
            _globalControl.savedScenes[_sceneID] = new SceneData();
        }
    }

    void OnDestroy()
    {
        if (
            _globalControl.savedScenes[_sceneID]
                .saveTasksProgression
                .tasksAreCompleted &&
            _globalControl.higherSceneCompletedID < _sceneID
        )
        {
            _globalControl.higherSceneCompletedID = _sceneID;
        }
        _globalControl.currentSceneID = _sceneID;
    }

}
