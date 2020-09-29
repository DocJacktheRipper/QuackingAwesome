using Props.spawning;
using UnityEngine;

namespace OldStuffNeedsToBeDeleted
{
    public class OldPeaCollecting : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            PlayerIsTrigger(other);
        }

        // Checks if player was trigger. If so, checks if the duck can carry more sticks.
        // If so, collect it. Otherwise, leave it.
        private bool PlayerIsTrigger(Collider other)
        {
            OldInventory oldInventory = other.GetComponent<OldInventory>();

            if (oldInventory == null)
            {
                Debug.Log("It wasn't the Duck! (pea)");
                return false;
            }

            oldInventory.Pb.BarValue += 1;
            Destroy(gameObject);
        
            // spawn another pea
            var nbContainer = GameObject.Find("SpawningBehaviour");
            var spawner = nbContainer.GetComponent<PeaSpawner>();
            spawner.Spawn();

            return true;
        }
    }
}
