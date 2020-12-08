#define VERBOSE

using UnityEngine;

namespace Tasks.TaskUpdater
{
    public class Level_3_Tasks : TasksUpdater
    {
        private void Start()
        {
            // if it was loaded from save, do not init
            if (LoadSave())
                return;
            
#if VERBOSE
            Debug.Log("--- Tasks are created ---");
#endif
            
            // add the global tasks to the current level specialized ones
            AddGlobalTasks();
        }
    }
}