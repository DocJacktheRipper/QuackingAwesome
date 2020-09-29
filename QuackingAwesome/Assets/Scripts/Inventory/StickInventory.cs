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
            for (var i = 0; i < (n-overflow); i++)
            {
                ShowStickInDuckbill();
            }
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
                if (transform.childCount <= 0)
                {
                    Debug.Log("No more children in duck");
                    return;
                }
                
                var child = transform.GetChild(i).gameObject;
                Destroy(child);
            }
        }

    
        public void DeleteAllVisualSticks()
        {
            DeleteVisualSticks(transform.childCount);
        }

        private void ShowStickInDuckbill()
        {
            if (!enableDuckbillVisual)
            {
                return;
            }
        
            var stick = Instantiate(branchVisual, transform, true);
            stick.transform.localPosition = new Vector3(0.00037f, 0.00869f, 0.00588f);
            stick.transform.eulerAngles = new Vector3(0f, -90f, 0f);
        }
        
        public int GetNumberOfSticks()
        {
            return numberOfSticks;
        }
    }
}
