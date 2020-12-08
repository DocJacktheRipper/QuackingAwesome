using System.Collections.Generic;
using Inventory;

namespace Tasks.TaskUpdater
{
    public class Level_1_Tasks : TasksUpdater
    {
        public StickInventory stickInventory;
        private new void Start()
        {
            levelTasks = new List<Task>();
            
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
        
            // Add the specific tasks of the current level to the global ones
            base.Start();

        }

    }
}
