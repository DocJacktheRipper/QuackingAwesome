using AI.Beaver;
using Inventory;
using Props.spawning;
using UnityEngine;

namespace Props
{
    public class StickCollecting : MonoBehaviour
    {
        private Animator _duckAnimator;
        private static readonly int DoPickAndKeep = Animator.StringToHash("DoPickAndKeep");

        void Start()
        {
            _duckAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        }
        
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
        
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collider activated - stick");
            if (PlayerIsTrigger(other.collider))
            {
                return;
            }
            if (BeaverDelete(other.collider))
            {
                return;
            }
            /*if (BeaverIsTrigger(other))
            {
                return;
            }*/    
        }

        // Checks if player was trigger. If so, checks if the duck can carry more sticks.
        // If so, collect it. Otherwise, leave it.
        private bool PlayerIsTrigger(Collider other)
        {
            StickInventory inventory = other.GetComponent<StickInventory>();

            if (inventory == null)
            {
                //Debug.Log("It wasn't the Duck! (stick)");
                return false;
            }

            if (inventory.AddStick())
            {
                _duckAnimator.SetTrigger(DoPickAndKeep);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Can't collect any more sticks. I'm a frikkin duck!");
            }

            return true;
        }

        //Checks if the beaverNavigation was a trigger and sends the position of focused stick to BeaverAI.cs
        //Automatically searches for a new target if a stick has been taken by the duck before reaching it
        //Larger "CapsuleCollider" on "Beaver" is used only as a trigger of an area and it has no collision with the playable duck
        private bool BeaverIsTrigger(Collider other)
        {
            BeaverAI beaverAI = GameObject.FindGameObjectWithTag("Beaver").GetComponent<BeaverAI>();

            if (other.gameObject.CompareTag("Beaver"))
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
            if (other.gameObject.CompareTag("BeaverTrigger"))
            {
                Destroy(gameObject);
                GameObject.Find("SpawningBehaviour").GetComponent<StickSpawner>().SpawnWithDelay(3);
                Debug.Log("Deleted a stick");
            }
            if (other.gameObject.CompareTag("Beaver"))
            {
                BeaverIsTrigger(other);
            }
            return true;
        }
    }
}
