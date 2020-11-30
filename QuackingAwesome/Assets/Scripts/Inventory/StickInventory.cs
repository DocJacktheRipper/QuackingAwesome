using Spawning.Props.PropTrigger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class StickInventory : MonoBehaviour
    {
        public bool collectingEnabled = true;
        
        public int numberOfSticks;
        public int maxCapacityOfSticks = 1;

        public Transform carriedSticks;
        public float dropRadius = 1.5f;

        private Transform _stickEnvironmentContainer;

        private void Start()
        {
            //carriedSticks = transform.Find("CarriedSticks");
            _stickEnvironmentContainer = GameObject.Find("CollectableSicks").transform;
        }

        #region Adding

        public bool AddStick(Transform stick)
        {
            var success = AddStick();

            if (success)
            {
                stick.SetParent(carriedSticks);
            }

            return success;
        }

        public bool AddStick()
        {
            return AddSticks(1) <= 0;
        }

        /// <summary>
        /// Add sticks to Ducks inventory as number, if enough capacity.
        /// </summary>
        /// <param name="n">sticks to transfer</param>
        /// <returns>not collectable sticks</returns>
        public int AddSticks(int n)
        {
            var overflow = TransferSticks(n);
            
            return overflow;
        }

        #endregion

        #region RemoveFromInventory

        public void RemoveStick(int amount)
        {
            numberOfSticks -= amount;
            if (numberOfSticks < 0)
                numberOfSticks = 0;
        }

        private int TransferSticks(int n)
        {
            numberOfSticks += n;
            if (numberOfSticks <= maxCapacityOfSticks) 
                return 0;
        
            var overflow = numberOfSticks - maxCapacityOfSticks;
            numberOfSticks = maxCapacityOfSticks;
            return overflow;
        }
        
        public void MoveStickToNest(Transform position)
        {
            // change parent and invoke animation
            var stick = carriedSticks.GetChild(0);
            stick.SetParent(position, true);
            RemoveStick(1);

            // determine position
            var pos = position.position;
            pos.y += 0.4f;

            // move and delete stick at the end
            stick.GetComponent<StickTriggerEnter>().MoveStickToPos(pos, true);
        }

        public void MoveSticksToNest(int number, Transform position)
        {
            numberOfSticks -= number;
            if (numberOfSticks < 0)
                numberOfSticks = 0;

            for (int i = 0; i < number; i++)
            {
                if(carriedSticks.childCount > 0)
                    MoveStickToNest(position);
            }
        }

        public void DropSticks(int number)
        {
            for (int i = 0; i < number && (carriedSticks.childCount > 0); i++)
            {
                DropStick();
            }
        }

        public void DropStick()
        {
            if (carriedSticks.childCount <= 0)
                return;

            // drop it within radius
            var targetPos = (Vector3) Random.insideUnitCircle;
            targetPos.Normalize();
            targetPos *= dropRadius;
            targetPos.z = targetPos.y;
            // around Inventory
            targetPos += transform.position;
            targetPos.y = 0f;    

            // set parent
            var stick = carriedSticks.GetChild(0).GetComponent<StickTriggerEnter>();
            stick.transform.SetParent(_stickEnvironmentContainer, true);
            
            RemoveStick(1);
            
            // invoke dropping
            stick.MoveStickToPos(targetPos, false);
        }

        #endregion

        public int GetNumberOfSticks()
        {
            return numberOfSticks;
        }
    }
}
