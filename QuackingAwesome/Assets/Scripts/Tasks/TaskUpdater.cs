using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TasksUpdater : MonoBehaviour
{
    public bool tasksAreCompleted = false;

    public List<Task> levelTasks = new List<Task>();

    private TasksProgression _savedTasksProgression;
    private bool _initialize = false;
    
    public GameObject nestsParent;
    
    public void Start()
    {
        #region add global tasks
        levelTasks.Add(new BuildAllNests(nestsParent));
        #endregion

        #region load save
        _savedTasksProgression = GlobalControl.Instance.savedGame
            .savedScenes[SceneManager.GetActiveScene().buildIndex]
            .saveTasksProgression;
        // load saved data
        tasksAreCompleted = _savedTasksProgression.tasksAreCompleted;

        // if the tasks have already been completed it is not useful to keep updating them
        if (tasksAreCompleted) return;
   
        if (_savedTasksProgression.levelTasks == null)
        {
            _initialize = true;
            _savedTasksProgression.levelTasks = new List<Task>(levelTasks.Count);
        }
        if (!_initialize)
            levelTasks = _savedTasksProgression.levelTasks;
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
        _savedTasksProgression.tasksAreCompleted = TasksAreCompleted();
        if (!tasksAreCompleted) _savedTasksProgression.levelTasks = levelTasks;
    }
}
