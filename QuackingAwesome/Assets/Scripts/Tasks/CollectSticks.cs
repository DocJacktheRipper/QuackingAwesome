using Inventory;
using UnityEngine;

namespace Tasks
{
    public class CollectSticks : Task
    {
        private StickInventory _stickInventory;

        public CollectSticks(StickInventory stickInventory)
        {
            description = "Collect sticks";
        
            _stickInventory = stickInventory;
        }

        public override bool UpdateProgression()
        {
            progression = _stickInventory.GetNumberOfSticks();
            return IsCompleted();
        }
    }
}
