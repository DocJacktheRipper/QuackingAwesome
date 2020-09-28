using UnityEngine;

namespace Props
{
    public class StickCollecting : MonoBehaviour
    {
        public bool enableDuckbillVisual;
        public GameObject branchVisual;
    
        private void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
        }

        // Checks if player was trigger. If so, checks if the duck can carry more sticks.
        // If so, collect it. Otherwise, leave it.
        private bool PlayerIsTrigger(Collider other)
        {
            Inventory inventory = other.GetComponent<Inventory>();

            if (inventory == null)
            {
                Debug.Log("It wasn't the Duck! (stick)");
                return false;
            }

            if (inventory.numberOfSticks < inventory.maxCapacityOfSticks)
            {
                inventory.numberOfSticks++;
                Destroy(gameObject);
                if (enableDuckbillVisual)
                {
                    ShowSticksInDuckbill(inventory);
                }
            }
            else
            {
                Debug.Log("Can't collect any more sticks. I'm a frikkin duck!");
            }

            return true;
        }

        private void ShowSticksInDuckbill(Inventory inventory)
        {
            var stick = Instantiate(branchVisual, inventory.transform, true);
            stick.transform.localPosition = new Vector3(0.00037f, 0.00869f, 0.00588f);
            //stick.transform.Rotate(0f, -90f, 0f, Space.Self);
            stick.transform.eulerAngles = new Vector3(0f, -90f, 0f);
        }
    }
}
