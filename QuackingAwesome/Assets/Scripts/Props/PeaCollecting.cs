using UnityEngine;

public class PeaCollecting : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerIsTrigger(other);
    }

    // Checks if player was trigger. If so, checks if the duck can carry more sticks.
    // If so, collect it. Otherwise, leave it.
    private bool PlayerIsTrigger(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.Log("It wasn't the Duck! (pea)");
            return false;
        }

        inventory.Pb.BarValue += 1;
        Destroy(gameObject);

        return true;
    }
}
