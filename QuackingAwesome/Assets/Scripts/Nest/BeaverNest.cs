using AI.Beaver.StateMachine_Beaver;
using Inventory;
using UnityEngine;

namespace Nest
{
    public class BeaverNest : MonoBehaviour
    {
        public int sticks;
        
        private void OnTriggerEnter(Collider other)
        {
            if (BeaverWasTrigger(other))
            {
                return;
            }
        }

        private bool BeaverWasTrigger(Collider other)
        {
            if (!other.CompareTag("Beaver"))
            {
                return false;
            }

            var beaver = other.transform.parent;
            
            // move sticks
            var inventory = beaver.Find("CarriedSticks").GetComponent<StickInventory>();
            sticks += TransferSticksToNest(inventory);
            
            // change state
            var stateHandler = beaver.Find("AI").GetComponent<StateHandlerBeaver>();
            stateHandler.ChangeState(stateHandler.idle);

            return true;
        }

        private int TransferSticksToNest(StickInventory inventory)
        {
            var number = inventory.numberOfSticks;
            
            inventory.MoveSticksToNest(number, transform);

            return number;
        }
    }
}
