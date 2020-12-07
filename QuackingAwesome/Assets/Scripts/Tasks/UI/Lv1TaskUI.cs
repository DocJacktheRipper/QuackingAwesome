using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tasks.UI
{
    public class Lv1TaskUI : MonoBehaviour
    {
        public TasksUpdater _tasksUpdater;
        private List<Task> _levelTasks;
        public List<GameObject> uiTasks;
        private List<Text> _uiProgressionTexts;
        private bool[] _tasksCompleted;
    
        void Start()
        {
            _levelTasks = _tasksUpdater.levelTasks;
        
            InitTasks();
        }

        void Update()
        {
            UpdateProgression();
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

            if (_levelTasks[index].IsCompleted())
            {
                _tasksCompleted[index] = true;
                
                uiTasks[index].transform.Find("Checkmark").gameObject.SetActive(true);
            }
        }
    }
}
