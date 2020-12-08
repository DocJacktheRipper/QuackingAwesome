using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace Tasks.TaskUpdater
{
    public class Level_2_Tasks : TasksUpdater
    {
        public StickInventory stickInventory;
        
        private void Start()
        {
            Debug.Log("LEVEL 2 TASKS");
            
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
