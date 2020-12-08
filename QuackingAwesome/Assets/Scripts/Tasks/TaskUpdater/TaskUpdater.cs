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

        public GameObject nestsParent;

        public bool LoadSave()
        {
            #region load save
            LoadTaskProgressionFromSave();

            return LoadTasksFromSave();
            #endregion
        }

        private void LoadTaskProgressionFromSave()
        {
            _savedTasksProgression = GlobalControl.Instance.savedGame
                .savedScenes[SceneManager.GetActiveScene().buildIndex]
                .saveTasksProgression;
        }

        private bool LoadTasksFromSave()
        {
            if (_savedTasksProgression.levelTasks == null || _savedTasksProgression.levelTasks.Count <= 0)
            {
#if VERBOSE
                Debug.Log("--- Saved level tasks are initialized ---");
#endif
                _savedTasksProgression.levelTasks = new List<Task>();
                return false;
            }

            else
            {
#if VERBOSE
                Debug.Log("--- Tasks were loaded by save... ---");               
#endif
                // load saved data
                levelTasks = _savedTasksProgression.levelTasks;
                tasksAreCompleted = _savedTasksProgression.tasksAreCompleted;
                return true;
            }
        }

        protected void AddGlobalTasks()
        {
            levelTasks.Add(new BuildAllNests(nestsParent));
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
            _savedTasksProgression.tasksAreCompleted = tasksAreCompleted;
            if (!tasksAreCompleted) _savedTasksProgression.levelTasks = levelTasks;
        }
    }
}
