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
        
        /*
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
        */

        /*
        private void ShowStickInDuckbill()
        {
            if (!enableDuckbillVisual)
            {
                return;
            }
        
            var stick = Instantiate(branchVisual, _carriedSticks, false);
            //stick.transform.localPosition = new Vector3(-0.0144f, 0.1668f, 0.2831f);
            //stick.transform.eulerAngles = new Vector3(0f, -90f, 0f);
            //stick.transform.localRotation = Quaternion.Euler(0,90,0);
            
        }*/
        
        public int GetNumberOfSticks()
        {
            return numberOfSticks;
        }
    }
}
