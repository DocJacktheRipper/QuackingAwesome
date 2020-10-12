using Inventory;
using Props.spawning;
using UnityEngine;

namespace Props
{
    public class PeaCollecting : MonoBehaviour
    {
        public int nutritiousValue = 1;
            
        private void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (PlayerIsTrigger(other.collider))
            {
                return;
            }
        }

        // Checks if player was trigger. If so, checks if the duck can carry more sticks.
        // If so, collect it. Otherwise, leave it.
        private bool PlayerIsTrigger(Collider other)
        {
            EnergyInventory inventory = other.GetComponent<EnergyInventory>();

            if (inventory == null)
            {
                //Debug.Log("It wasn't the Duck! (pea)");
                return false;
            }

            inventory.IncreaseEnergy(nutritiousValue);
            Destroy(gameObject);
        
            // spawn another pea
            var nbContainer = GameObject.Find("SpawningBehaviour");
            var spawner = nbContainer.GetComponent<PeaSpawner>();
            spawner.Spawn();

            return true;
        }
    }
}
