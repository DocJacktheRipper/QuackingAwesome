using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TasksUpdater : MonoBehaviour
{
    public bool tasksAreCompleted = false;

    public List<Task> levelTasks = new List<Task>();

    private SceneData _savedScene;
    
    public GameObject nestsParent;
    
    public void Start()
    {
        #region add global tasks
        levelTasks.Add(new BuildAllNests(nestsParent));
        #endregion

        #region load save
        _savedScene = GlobalControl.Instance.savedPlayerData.currentScene;
        
        // if the player loaded the scene he was previously playing, load his progression data
        if (_savedScene.id == SceneManager.GetActiveScene().buildIndex)
        {
            // load saved data
            tasksAreCompleted = _savedScene.saveTasksProgression.tasksAreCompleted;

            // if the tasks have already been completed it is not useful to keep updating them
            if (tasksAreCompleted) return;
        
            // if not load the progression
            for (int i = 0; i < levelTasks.Count; i++)
            {
                levelTasks[i].isCompleted = _savedScene.saveTasksProgression.levelTasks[i].isCompleted;
                levelTasks[i].progression = _savedScene.saveTasksProgression.levelTasks[i].progression;
            }
        }
        #endregion
        

    }
    
    public bool TasksAreCompleted()
    {
        foreach (var task in levelTasks)
            if (!task.isCompleted && !task.Update())
                return false;
        return true;
    }
    
    private void Update()
    {
        if (!tasksAreCompleted)
            tasksAreCompleted = TasksAreCompleted();
    }


    // save the tasks progression
    private void OnDestroy()
    {
        _savedScene.saveTasksProgression.tasksAreCompleted = TasksAreCompleted();
        if (!tasksAreCompleted) _savedScene.saveTasksProgression.levelTasks = levelTasks;
    }
}
