using UnityEngine;

namespace Inventory
{
    public class StickInventory : MonoBehaviour
    {
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
                if (_carriedSticks.childCount <= 0)
                {
                    Debug.Log("No more children in duck");
                    return;
                }
                
                var child = _carriedSticks.GetChild(i).gameObject;
                Destroy(child);
            }
        }

    
        public void DeleteAllVisualSticks()
        {
            DeleteVisualSticks(_carriedSticks.childCount);
        }

        private void ShowStickInDuckbill()
        {
            if (!enableDuckbillVisual)
            {
                return;
            }
        
            var stick = Instantiate(branchVisual, _carriedSticks, true);
            stick.transform.localPosition = new Vector3(0.01237f, 0.07559f, 0.04857f);
            stick.transform.eulerAngles = new Vector3(0f, -90f, 0f);
        }
        
        public int GetNumberOfSticks()
        {
            return numberOfSticks;
        }
    }
}
