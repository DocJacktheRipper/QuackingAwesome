using AI.Beaver.StateMachine_Beaver;
using Inventory;
using UnityEngine;

namespace Nest.NPC_Nests
{
    public class BeaverNest : MonoBehaviour
    {
        public int sticks;
        
        /*
        private void OnTriggerEnter(Collider other)
        {
            if (BeaverWasTrigger(other))
            {
                return;
            }
        }

        private void OnTriggerStay(Collider other)
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

            if (other.name.Contains("Body") || other.name.Contains("Detection"))
            {
               //s Debug.Log("Trigger: "+ other.name);
                return true;
            }
            
            var ai = other.transform.parent;
            
            // move sticks
            var inventory = ai.parent.Find("CarriedSticks").GetComponent<StickInventory>();
            sticks += TransferSticksToNest(inventory);
            
            // change state
            var stateHandler = ai.GetComponent<StateHandlerBeaver>();
            stateHandler.ChangeState(stateHandler.idle);

            return true;
        }
        */

        public int TransferSticksToNest(StickInventory inventory)
        {
            var number = inventory.numberOfSticks;
            
            // Debug.Log("Moving stick to nest");
            inventory.MoveSticksToNest(number, transform);

            return number;
        }
    }
}
