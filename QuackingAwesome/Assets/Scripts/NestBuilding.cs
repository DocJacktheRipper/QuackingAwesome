using UnityEngine;
using UnityEngine.UI;

public class NestBuilding : MonoBehaviour
{
    public int numberOfSticks = 0;
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

        if (player == null)
        {
            return;
        }

        // check for sticks in duck's inventory
        if (player.numberOfSticks > 0)
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
            display.text = "Sticks in Nest: " + numberOfSticks + "/" + neededSticks;
            
            if (numberOfSticks >= neededSticks)
            {
                Vector3 pos = this.gameObject.transform.position;
                pos = new Vector3(-305f, -25.15f, 25.4f);
                Instantiate(finishedNest, pos, Quaternion.identity);
            }
        }
    }
}
