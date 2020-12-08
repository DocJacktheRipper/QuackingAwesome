#define VERBOSE

using UnityEngine;

using Inventory;

namespace Tasks.TaskUpdater
{
    public class Level_1_Tasks : TasksUpdater
    {
        public StickInventory stickInventory;
        private void Start()
        {
            // if it was loaded from save, do not init
            if (LoadSave())
                return;
            
#if VERBOSE
            Debug.Log("--- Tasks are created ---");
#endif
            
            // add the specific tasks of the current level to the global ones
            levelTasks.Add(new CollectSticks(stickInventory)
            {
                description = "Collect a stick",
                goal = 1
            });
        
            levelTasks.Add(new SticksToRock(nestsParent)
            {
                description = "Bring a stick to a rock",
                goal = 1
            });
            
            // add the global tasks to the current level specialized ones
            AddGlobalTasks();
        }

    }
}
