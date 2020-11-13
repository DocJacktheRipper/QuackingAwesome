using System;
using AI.Beaver;
using AI.Beaver.StateMachine_Beaver;
using Inventory;
using Props.spawning;
using UnityEditor;
using UnityEngine;

namespace Props.Sticks
{
    public class StickTriggerEnter : MonoBehaviour
    {
        private Collider _trigger;
        
        // where to put the position before delete
        public Transform positionPool;
        private Transform _duckCarriedSticks;
        
        // target position [e.g. nest]
        private bool _shouldMove;
        private float speed = 1.2f;
        private Vector3 targetPos;
        private bool _deleteOnPositionReached = false;
        
        // animation
        private Animator _duckAnimator;
        private static readonly int DoPickAndKeep = Animator.StringToHash("DoPickAndKeep");

        void Start()
        {
            _trigger = gameObject.GetComponent<Collider>();
            var duck = GameObject.FindWithTag("Player");
            _duckAnimator = duck.GetComponent<Animator>();
            
            _duckCarriedSticks = duck.transform.Find("CarriedSticks");
            positionPool = GameObject.Find("StickSpawnSpots").transform;
        }

        private void Update()
        {
            if (_shouldMove)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if(Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    _shouldMove = false;
                    if (_deleteOnPositionReached)
                    {
                        // delete this stick
                        DeleteStick();
                    }
                    
                    // enable trigger again
                    _trigger.enabled = true;
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
            _trigger.enabled = false;
            
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
            /// to delete later //
            /*
            var beaverAI = GameObject.FindGameObjectWithTag("Beaver").GetComponent<BeaverAI>();

            if (other.CompareTag("Beaver"))
            {
                Vector3 stickPosition = this.transform.position;
                beaverAI.FetchStick(stickPosition);
            }
            /// end to delete /*/

            /*
            var beaver = other.GetComponent<StateHandlerBeaver>();
            if (beaver == null)
                return false;
            
            Debug.Log("Detected by stick");
            beaver.state.StickDetected(transform);
            */
            
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

        /// <summary>
        /// Invoke moving of Stick until reach of position.
        /// </summary>
        /// <param name="targetPosition">Position to reach</param>
        /// <param name="deleteStick">Determines, if deleted after reaching position</param>
        public void MoveStickToPos(Vector3 targetPosition, bool deleteStick)
        {
            targetPos = targetPosition;
            _shouldMove = true;
            _deleteOnPositionReached = deleteStick;
        }

        public void DeleteStick()
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).parent = positionPool;
            }
            Destroy(gameObject);
        }
    }
}
