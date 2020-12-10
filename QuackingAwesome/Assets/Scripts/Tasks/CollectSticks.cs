using System;
using Inventory;

namespace Tasks
{
    [Serializable]
    public class CollectSticks : Task
    {
        [NonSerialized]
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
