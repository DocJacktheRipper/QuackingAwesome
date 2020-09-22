using UnityEngine;
using UnityEngine.UI;

public class NestBuilding : MonoBehaviour
{
    public int numberOfSticks;
    public int neededSticks;
    public Text display;

    public GameObject finishedNest;

    private void OnTriggerEnter(Collider other)
    {
        PlayerIsTrigger(other);
    }

    private void PlayerIsTrigger(Collider other)
    {
        Inventory player = other.GetComponent<Inventory>();

        // does player-inventory exist?
        if (player == null)
        {
            return;
        }
        // is already built a nest on rock?
        if (transform.childCount > 0)
        {
            return;
        }

        // check for sticks in duck's inventory and needed for upgrade
        if (player.numberOfSticks > 0)
        {
            TransferSticks(player);
            
            PrintText();
            
            if (numberOfSticks >= neededSticks)
            {
                BuildNest();
            }
        }
    }

    private void TransferSticks(Inventory player)
    {
        // only use as much sticks as needed for the nest
        int diff = neededSticks - numberOfSticks;
        if ((diff - player.numberOfSticks) < 0)
        {
            numberOfSticks = neededSticks;
            player.numberOfSticks -= diff;
        }
        else
        {
            numberOfSticks += player.numberOfSticks;
            player.numberOfSticks = 0;
        }
    }

    private void BuildNest()
    {
        Vector3 pos = gameObject.transform.localPosition;
        pos = new Vector3(-244.35f, 0.66f, -12.6f);
        GameObject nestOfSticks = Instantiate(finishedNest, pos, Quaternion.identity);
        nestOfSticks.transform.parent = transform;
    }

    private void PrintText()
    {
        display.text = "Sticks in Nest: " + numberOfSticks + "/" + neededSticks;
    }
}
