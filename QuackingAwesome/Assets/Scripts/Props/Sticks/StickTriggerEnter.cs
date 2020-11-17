using System;
using AI.Beaver.StateMachine_Beaver;
using Inventory;
using Props.spawning;
using UnityEngine;

namespace Props.Sticks
{
    public class StickTriggerEnter : MonoBehaviour
    {
        private Collider _trigger;
        private Collider _collider;
        
        // where to put the position before delete
        public Transform positionPool;
        private Transform _duckCarriedSticks;
        
        // target position [e.g. nest]
        private bool _shouldMove;
        private float speed = 1.2f;
        private Vector3 _targetPos;
        private bool _deleteOnPositionReached;
        
        // animation
        private Animator _duckAnimator;
        private static readonly int DoPickAndKeep = Animator.StringToHash("DoPickAndKeep");

        void Start()
        {
            _trigger = gameObject.GetComponent<BoxCollider>();
            _collider = gameObject.GetComponent<SphereCollider>();
            var duck = GameObject.FindWithTag("Player");
            _duckAnimator = duck.GetComponent<Animator>();
            
            _duckCarriedSticks = duck.transform.Find("CarriedSticks");
            positionPool = GameObject.Find("StickSpawnSpots").transform;
        }

        private void Awake()
        {
            _trigger = gameObject.GetComponent<BoxCollider>();
            _collider = gameObject.GetComponent<SphereCollider>();
        }

        private void Update()
        {
            // lets stick move to set position and activate trigger again
            if (_shouldMove)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
                if(Vector3.Distance(transform.position, _targetPos) < 0.1f)
                {
                    _shouldMove = false;
                    if (_deleteOnPositionReached)
                    {
                        // delete this stick
                        DeleteStick();
                        return;
                    }
                    
                    // enable trigger again
                    _trigger.enabled = true;
                    _collider.enabled = false;
                }
            }
        }

        #region Trigger

        private void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
            // if (BeaverDelete(other))
            // {
            //     return;
            // }
            if (BeaverIsTrigger(other))
            {
                return;
            }
        }

        
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Stick collided with: " + other.collider.name);
            if (BeaverIsTrigger(other.collider))
            {
                return;
            }
        }
        

        #region PlayerTrigger

        // Checks if player was trigger. If so, checks if the duck can carry more sticks.
        // If so, collect it. Otherwise, leave it.
        private bool PlayerIsTrigger(Collider other)
        {
            StickInventory inventory = other.GetComponent<StickInventory>();

            if (!other.CompareTag("Player") || inventory == null)
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
                PickStick(_duckCarriedSticks);
                // animation for stick picking
                _duckAnimator.SetTrigger(DoPickAndKeep); //TODO: make dynamically for other animals
            }
            else
            {
                Debug.Log("Can't collect any more sticks. I'm a frikkin duck!");
            }

            return true;
        }

        #endregion

        #region BeaverTrigger

        /// <summary>
        /// Checks, if Beaver's Detection has triggered collider. If so, invoke Beaver to fetch the stick.
        /// </summary>
        /// <returns>Beaver was Collider.</returns>
        private bool BeaverHasDetected(Collider other)
        {
            if (!other.GetComponent<Collider>().isTrigger || !other.CompareTag("Beaver"))
            {
                return false;
            }
            
            Debug.Log("Beaver has triggered a Stick!");
            // Beaver was the collider.
            // Get the stateHandler of the Beaver and invoke Trigger
            //other.transform.parent.GetComponentInChildren<StateHandlerBeaver>();
            Component component;
            if (other.transform.parent.TryGetComponent(typeof(StateHandlerBeaver), out component))
            {
                var stateHandler = component.GetComponent<StateHandlerBeaver>();
                stateHandler.StickDetected(transform);
            }
            
            return true;
        }

        //Checks if the beaverNavigation was a trigger and sends the position of focused stick to BeaverAI.cs
        //Automatically searches for a new target if a stick has been taken by the duck before reaching it
        //Larger "CapsuleCollider" on "Beaver" is used only as a trigger of an area and it has no collision with the playable duck
        private bool BeaverIsTrigger(Collider other)
        {
            if (!other.CompareTag("Beaver"))
            {
                return false;
            }

            // ignore if only the body triggered
            if (other.name.Contains("Body"))
            {
                Debug.Log("Body triggered");
                // TODO: do behaviour here
                return true;
            }

            var ai = other.transform.parent;
            var tempStickBox = ai.parent.Find("CarriedSticks");
            
            Component component;
            if(!tempStickBox.TryGetComponent(typeof(StickInventory), out component))
            {
                Debug.Log("No component found");
                return false;
            }
            var inventory = component.GetComponent<StickInventory>();
            
            if (!inventory.collectingEnabled || (inventory.numberOfSticks >= inventory.maxCapacityOfSticks))
            {
                Debug.Log("Stick not collectable.");
                return false;
            }

            if (inventory.AddStick())
            {
                PickStick(inventory.transform);
                var stateHandlerBeaver = ai.GetComponent<StateHandlerBeaver>();
                stateHandlerBeaver.StickCollected();
            }
            else
            {
                Debug.Log("Beaver can't collect any more sticks.");
            }
            
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
        
        
        #endregion
        
        

        #endregion
        #region HelperMethods
        
        public void PickStick(Transform targetParent)
        {
            // disable trigger
            _trigger.enabled = false;
            _collider.enabled = false;

            // move to duck
            Transform transform1;
            (transform1 = transform).SetPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
            transform1.SetParent(targetParent, false);
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
            _targetPos = targetPosition;
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

        #endregion
       
    }
}
