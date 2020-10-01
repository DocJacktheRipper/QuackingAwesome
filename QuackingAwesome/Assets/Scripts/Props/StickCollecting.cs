using Boo.Lang;
using Inventory;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Props
{
    public class StickCollecting : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
            if (BeaverIsTrigger(other))
            {
                return;
            }
            if (BeaverDelete(other))
            {
                return;
            }

        }

        // Checks if player was trigger. If so, checks if the duck can carry more sticks.
        // If so, collect it. Otherwise, leave it.
        private bool PlayerIsTrigger(Collider other)
        {
            StickInventory inventory = other.GetComponent<StickInventory>();

            if (inventory == null)
            {
                Debug.Log("It wasn't the Duck! (stick)");
                return false;
            }

            if (inventory.AddStick())
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Can't collect any more sticks. I'm a frikkin duck!");
            }

            return true;
        }


        
        private bool BeaverIsTrigger(Collider other)
        {
            BeaverAI beaverAI = GameObject.FindGameObjectWithTag("Beaver").GetComponent<BeaverAI>();
           
            if (other.gameObject.tag == "Beaver")
            {
                Vector3 stickPosition = this.transform.position;
                beaverAI.FetchStick(stickPosition);
            } 
            return true;
        }

        private bool BeaverDelete(Collider other)
        {
            if (
                other.gameObject.tag == "BeaverTrigger" &&
                other.gameObject.tag == "Beaver")
            {
                Debug.Log("yes");
            }
            return true;
        }
    }
}
