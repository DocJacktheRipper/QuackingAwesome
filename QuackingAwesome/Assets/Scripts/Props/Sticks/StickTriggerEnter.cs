using System;
using AI.Beaver;
using Inventory;
using Props.spawning;
using UnityEditor;
using UnityEngine;

namespace Props.Sticks
{
    public class StickTriggerEnter : MonoBehaviour
    {
        private Animator _duckAnimator;
        private static readonly int DoPickAndKeep = Animator.StringToHash("DoPickAndKeep");
        
        // where to put the position before delete
        public Transform positionPool;
        private Transform _duckCarriedSticks;
        
        // target position [e.g. nest]
        private bool shouldMove;
        private float speed = 1.2f;
        private Vector3 targetPos;


        void Start()
        {
            var duck = GameObject.FindWithTag("Player");
            _duckAnimator = duck.GetComponent<Animator>();
            
            _duckCarriedSticks = duck.transform.Find("CarriedSticks");
            positionPool = GameObject.Find("StickSpawnSpots").transform;
        }

        private void Update()
        {
            if (shouldMove)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if(Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    // delete this stick
                    DeleteStick();
                }
            }
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

            if (!inventory.collectingEnabled)
            {
                return true;
            }

            if (inventory.AddStick())
            {
                PickStick();
            }
            else
            {
                Debug.Log("Can't collect any more sticks. I'm a frikkin duck!");
            }

            return true;
        }

        private void PickStick()
        {
            //_duckAnimator.SetTrigger(DoPickAndKeep);
            
            // disable trigger
            gameObject.GetComponent<Collider>().enabled = false;
            
            // animation for stick picking?
            
            
            // move to duck
            Transform transform1;
            (transform1 = transform).SetPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
            transform1.SetParent(_duckCarriedSticks, false);
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

        public void MoveStickToPos(Transform targetPosition)
        {
            targetPos = targetPosition.position;
            targetPos.y += 0.4f;
            shouldMove = true;
        }

        public void DeleteStick()
        { 
            transform.GetChild(0).parent = positionPool;
            Destroy(gameObject);
        }
    }
}
