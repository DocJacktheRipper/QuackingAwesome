using System.Collections.Generic;
using Inventory;

namespace Tasks.TaskUpdater
{
    public class Level_1_Tasks : TasksUpdater
    {
        public StickInventory stickInventory;
        private new void Start()
        {
            base.Start();
            
            // if it was loaded from save, do not init
            if (!Initialize)
                return;
            
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
            
            // Add the global tasks to the current level specialized ones
            AddGlobalTasks();
        }

    }
}
