#define VERBOSE

using System.Collections.Generic;
using LeavingScene.Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tasks.TaskUpdater
{
    public class TasksUpdater : MonoBehaviour
    {
        public bool tasksAreCompleted;

        public List<Task> levelTasks;

        private TasksProgression _savedTasksProgression;
        protected bool Initialize;
    
        public GameObject nestsParent;

        public void Start()
        {
            #region load save
            LoadTaskProgressionFromSave();

            // if the tasks have already been completed it is not useful to keep updating them
            if (tasksAreCompleted) return;

            if (LoadTasksFromSave())
            {
#if VERBOSE
                Debug.Log("--- Tasks were loaded by save... ---");               
#endif
                return;
            }
#if VERBOSE
            Debug.Log("--- Tasks are created ---");
#endif
            #endregion
        }

        private void LoadTaskProgressionFromSave()
        {
            _savedTasksProgression = GlobalControl.Instance.savedGame
                .savedScenes[SceneManager.GetActiveScene().buildIndex]
                .saveTasksProgression;
            // load saved data
            tasksAreCompleted = _savedTasksProgression.tasksAreCompleted;
        }

        private bool LoadTasksFromSave()
        {
            if (_savedTasksProgression.levelTasks == null || _savedTasksProgression.levelTasks.Count <= 0)
            {
                Initialize = true;
                _savedTasksProgression.levelTasks = new List<Task>(levelTasks.Count);
                return false;
            }
            if (!Initialize)
                levelTasks = _savedTasksProgression.levelTasks;

            return true;
        }

        protected void AddGlobalTasks()
        {
            if (Initialize)
            {
                levelTasks.Add(new BuildAllNests(nestsParent));
            }
        }

        private bool TasksAreCompleted()
        {
            bool completed = true;
            foreach (var task in levelTasks)
            {
                if(!task.isCompleted)
                    task.UpdateProgression();
                if(!task.isCompleted)
                    completed = false;
            }
            return completed;
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
}
