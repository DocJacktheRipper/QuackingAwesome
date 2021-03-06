﻿using AI.StateMachine;
using Inventory;
using Props.spawning;
using Spawning.Animals;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Beaver.StateMachine_Beaver
{
    public class BehaviourMethodsBeaver : BehaviourMethods
    {
        public StickSpawner stickSpawner;
        public float respawnDelayStick = 20;
        public StickInventory stickInventory;

        private NavMeshPath _path;
        private static readonly int DoDive = Animator.StringToHash("DoDive");

        private void Awake()
        {
            _path = new NavMeshPath();
        }

        #region Others

        public bool CollectStick(Collider other)
        {
            if (stickInventory.collectingEnabled)
            {
                //stickSpawner.RespawnWithDelay(respawnDelayStick);
                return stickInventory.AddStick(other.transform);
            }
            return false;
        }

        public void LetSticksFall()
        {
            stickInventory.DropSticks(stickInventory.GetNumberOfSticks());
        }

        public void DiveAndRespawnAtNest(float respawnTime)
        {
            var beaverObject = transform.parent.gameObject;
            // disable colliders
            foreach(var c in beaverObject.GetComponentsInChildren<Collider>())
            {
                c.enabled = false;
            }
            
            // dive
            animator.SetTrigger(DoDive);
            
            // Invoke respawn
            var spawner = GameObject.Find("SpawningBehaviour").GetComponent<BeaverSpawner>();
            if (spawner == null)
                return;
            spawner.RespawnWithDelay(respawnTime);
            
            // Delete BeaverObject after some time
            Invoke(nameof(DestroyBeaver), 2);
        }

        private void DestroyBeaver()
        {
            Destroy(transform.parent.gameObject);
        }


        #endregion

        #region Navigation

        public override void GotoNextPoint()
        {
            currentTarget = GetReachablePointInRadius();
            navigation.SetPath(_path);
        }
        
        private Vector3 GetReachablePointInRadius()
        {
            _path = new NavMeshPath();
            var position = transform.position;
            
            // calculate random position
            Vector3 randomDirection = Random.insideUnitSphere * randomSwimRadius; // random position within sphere
            randomDirection += position;       // sphere is around current object now
            randomDirection.y = 0;             // it shall not fly or be underwater

            // get closest point reachable from that position
            NavMesh.SamplePosition(randomDirection, out var hit, 3, 1);
            Vector3 finalPosition = hit.position;
 
            // check, if actually reachable
            NavMesh.CalculatePath(position, finalPosition, NavMesh.AllAreas, _path);
            if (_path.status == NavMeshPathStatus.PathComplete) 
            {
                //Debug.Log ("Valid path has been found");
                return finalPosition;
            } 
            else 
            {
                Debug.Log("No path to that position, picking a new point");
                //return GetReachablePointInRadius();
                return Vector3.zero;
            }
        }
        
        public bool CheckDestinationIsReachable(Vector3 position)
        {
            NavMesh.CalculatePath(transform.position, position, NavMesh.AllAreas, _path);
            if (_path.status == NavMeshPathStatus.PathComplete) 
            {
                //Debug.Log ("Valid path has been found");
                return true;
            } 
            else 
            {
                Debug.Log("No path to that position, picking a new point");
                //return GetReachablePointInRadius();
                return false;
            }
        }

        public override void Chase(Transform target)
        {
            if (CheckDestinationIsReachable(target.position))
            {
                base.Chase(target);
            }
            else
            {
                // if not reachable, set target to current position
                // so it switches next time automatically
                base.Chase(transform);
            }
        }

        public override void SetDestination(Vector3 dest)
        {
            if (CheckDestinationIsReachable(dest))
            {
                base.SetDestination(dest);
            }
            else
            {
                base.SetDestination(transform.position);
            }
        }

        #endregion
    }
}
