//using Boo.Lang;
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
            if (BeaverDelete(other))
            {
                return;
            }
            if (BeaverIsTrigger(other))
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

        //Checks if the beaver was a trigger and sends the position of focused stick to BeaverAI.cs
        //Automatically searches for a new target if a stick has been taken by the duck before reaching it
        //Larger "CapsuleCollider" on "Beaver" is used only as a trigger of an area and it has no collision with the playable duck
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
        //Checks if the inner "BoxCollider" is a trigger
        //If true, destroys the stick
        //If false, redirects to the "BeaverIsTrigger" function that searches for nearby sticks
        private bool BeaverDelete(Collider other)
        {
            if (other.gameObject.tag == "BeaverTrigger")
            {
                Destroy(gameObject);
                Debug.Log("Deleted a stick");
            }
            if (other.gameObject.tag == "Beaver")
            {
                BeaverIsTrigger(other);
            }
            return true;
        }
    }
}
