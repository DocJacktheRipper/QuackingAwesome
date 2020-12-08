using System.Collections.Generic;
using Inventory;

namespace Tasks.TaskUpdater
{
    public class Level_2_Tasks : TasksUpdater
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
            
            base.Start();
        }
    }
}
