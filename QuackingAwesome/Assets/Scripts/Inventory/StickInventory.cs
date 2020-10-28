using Props.Sticks;
using UnityEngine;

namespace Inventory
{
    public class StickInventory : MonoBehaviour
    {
        public bool collectingEnabled = true;
        
        public int numberOfSticks;
        public int maxCapacityOfSticks = 1;    
    
        public bool enableDuckbillVisual;
        public GameObject branchVisual;

        private Transform _carriedSticks;

        private void Start()
        {
            _carriedSticks = transform.Find("CarriedSticks");
        }

        public bool AddStick()
        {
            return AddSticks(1) <= 0;
        }

        /// <summary>
        /// Add sticks to Ducks inventory, if enough capacity.
        /// </summary>
        /// <param name="n">sticks to transfer</param>
        /// <returns>not collectable sticks</returns>
        public int AddSticks(int n)
        {
            var overflow = TransferSticks(n);
            /*
            for (var i = 0; i < (n-overflow); i++)
            {
                ShowStickInDuckbill();
            }
            */
            return overflow;
        }

        private int TransferSticks(int n)
        {
            numberOfSticks += n;
            if (numberOfSticks <= maxCapacityOfSticks) 
                return 0;
        
            var overflow = numberOfSticks - maxCapacityOfSticks ;
            numberOfSticks = maxCapacityOfSticks;
            return overflow;
        }
        
        public void MoveStickToNest(Transform position)
        {
            // change parent and invoke animation
            var stick = _carriedSticks.GetChild(0);
            stick.SetParent(position, true);

            // delete stick at the end
            stick.GetComponent<StickTriggerEnter>().MoveStickToPos(position);
        }

        public void MoveSticksToNest(int number, Transform position)
        {
            numberOfSticks -= number;
            if (numberOfSticks < 0)
                numberOfSticks = 0;

            for (int i = 0; i < number; i++)
            {
                if(_carriedSticks.childCount > 0)
                    MoveStickToNest(position);
            }
        }

        public int GetNumberOfSticks()
        {
            return numberOfSticks;
        }
    }
}
