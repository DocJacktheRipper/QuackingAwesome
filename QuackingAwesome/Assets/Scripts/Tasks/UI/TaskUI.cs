using System.Collections.Generic;
using Tasks.TaskUpdater;
using UI.Main_Menu.PauseMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Tasks.UI
{
    public class TaskUI : MonoBehaviour
    {
        public TasksUpdater tasksUpdater;
        private List<Task> _levelTasks;
        public List<GameObject> uiTasks;
        private List<Text> _uiProgressionTexts;
        private bool[] _tasksCompleted;
        
        public GameObject levelComplete;
        public GameObject tasksMenuBackground;
        private bool _completed; // to prevent triggering too often, save if completion event triggered once

        void Start()
        {
            tasksUpdater.Start();
            _levelTasks = tasksUpdater.levelTasks;
        
            InitTasks();
        }

        protected virtual void Update()
        {
            UpdateProgression();
            
            // check for completion
            CheckForCompletion();
        }

        private void CheckForCompletion()
        {
            if (_completed || !tasksUpdater.tasksAreCompleted) return;
            
            // Trigger CompletionEvent
            GetComponent<MainMenu>().OpenTask();
            levelComplete.SetActive(true);
            tasksMenuBackground.SetActive(false);
            _completed = true;
        }

        private void InitTasks()
        {
            _uiProgressionTexts = new List<Text>(uiTasks.Count);
            _tasksCompleted = new bool[uiTasks.Count];
        
            for (int i = 0; i < uiTasks.Count; i++)
            {
                var taskName = uiTasks[i].transform.Find("TaskName").GetComponent<Text>();
                taskName.text = _levelTasks[i].description;
            
                _uiProgressionTexts.Add(uiTasks[i].transform.Find("ProgressText").GetComponent<Text>());

                UpdateProgression(i);
            }
        }

        private void UpdateProgression()
        {
            for (int i = 0; i < uiTasks.Count; i++)
            {
                if (!_tasksCompleted[i])
                {
                    UpdateProgression(i);
                }
            }
        }

        private void UpdateProgression(int index)
        {
            var progr = _levelTasks[index].progression + "/" + _levelTasks[index].goal;
            _uiProgressionTexts[index].text = progr;

            if (_levelTasks[index].isCompleted)
            {
                _tasksCompleted[index] = true;
                
                uiTasks[index].transform.Find("Checkmark").gameObject.SetActive(true);
            }
        }
    }
}