using UnityEngine;

namespace Inventory
{
    public class StickInventory : MonoBehaviour
    {
        public int numberOfSticks;
        public int maxCapacityOfSticks = 1;    
    
        public bool enableDuckbillVisual;
        public GameObject branchVisual;

        private void Start()
        {
        }

        public bool AddStick()
        {
            if (numberOfSticks >= maxCapacityOfSticks) 
                return false;
        
            numberOfSticks++;
            return true;
        }

        public int GetNumberOfSticks()
        {
            return numberOfSticks;
        }

        /// <summary>
        /// Add sticks to Ducks inventory, if enough capacity.
        /// </summary>
        /// <param name="n">sticks to transfer</param>
        /// <returns>not collectable sticks</returns>
        public int AddSticks(int n)
        {
            var overflow = TransferSticks(n);
            ShowSticksInDuckbill();
            return overflow;
        }

        private int TransferSticks(int n)
        {
            numberOfSticks += n;
            if (numberOfSticks <= maxCapacityOfSticks) 
                return 0;
        
            var overflow = maxCapacityOfSticks - numberOfSticks;
            numberOfSticks = maxCapacityOfSticks;
            return overflow;
        }

        public void RemoveSticks(int number)
        {
            numberOfSticks -= number;
            if (numberOfSticks < 0)
                numberOfSticks = 0;
            
            DeleteVisualSticks(number);
        }
        public void DeleteVisualSticks(int number)
        {
            for (var i = 0; i < number; i++)
            {
                GameObject child = transform.GetChild(0).gameObject;
                if (child != null)
                {
                    Destroy(child);
                }
            }
        }

    
        public void DeleteAllVisualSticks()
        {
            DeleteVisualSticks(transform.childCount);
        }

        private void ShowSticksInDuckbill()
        {
            if (!enableDuckbillVisual)
            {
                return;
            }
        
            var stick = Instantiate(branchVisual, transform, true);
            stick.transform.localPosition = new Vector3(0.00037f, 0.00869f, 0.00588f);
            stick.transform.eulerAngles = new Vector3(0f, -90f, 0f);
        }
    }
}
