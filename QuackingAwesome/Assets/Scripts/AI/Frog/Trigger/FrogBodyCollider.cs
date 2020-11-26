using AI.Frog.StateMachine_Frog;
using Inventory;
using UnityEngine;

namespace AI.Frog.Trigger
{
    public class FrogBodyCollider : MonoBehaviour
    {
        public StateHandlerFrog stateHandler;
        public int energyValue;

        private void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
            stateHandler.MouthTriggerEntered(other);
        }

        private bool PlayerIsTrigger(Collider other)
        {
            var inventory = other.GetComponent<EnergyInventory>();
            if (inventory == null)
                return false;

            inventory.IncreaseEnergy(energyValue);

            DeleteFrog();
            
            return true;
        }

        private void DeleteFrog()
        {
            var frog = transform.parent.parent.gameObject;
            GameObject.Destroy(frog);
        }
    }
}
