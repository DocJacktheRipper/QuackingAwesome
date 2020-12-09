using Inventory;
using UnityEngine;

namespace Tasks
{
    public class CollectSticks : Task, ISerializationCallbackReceiver
    {
        private StickInventory _stickInventory;

        public void OnBeforeSerialize() {}
        public void OnAfterDeserialize() {}

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
