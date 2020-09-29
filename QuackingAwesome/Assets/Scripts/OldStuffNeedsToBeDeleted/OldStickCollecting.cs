using UnityEngine;

namespace OldStuffNeedsToBeDeleted
{
    public class OldStickCollecting : MonoBehaviour
    {
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
            OldInventory oldInventory = other.GetComponent<OldInventory>();

            if (oldInventory == null)
            {
                Debug.Log("It wasn't the Duck! (stick)");
                return false;
            }

            if (oldInventory.numberOfSticks < oldInventory.maxCapacityOfSticks)
            {
                oldInventory.numberOfSticks++;
                Destroy(gameObject);
                oldInventory.ShowSticksInDuckbill();
            }
            else
            {
                Debug.Log("Can't collect any more sticks. I'm a frikkin duck!");
            }

            return true;
        }

    }
}
