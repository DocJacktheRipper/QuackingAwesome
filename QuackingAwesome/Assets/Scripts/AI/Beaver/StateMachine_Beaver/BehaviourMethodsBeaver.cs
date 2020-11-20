using AI.StateMachine;
using Inventory;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Beaver.StateMachine_Beaver
{
    public class BehaviourMethodsBeaver : BehaviourMethods
    {
        public StickInventory stickInventory;

        private NavMeshPath _path;

        private void Awake()
        {
            _path = new NavMeshPath();
        }

        public void AddSpeed(float scaredAwayRunAwaySpeedBonus)
        {
            navigation.speed += scaredAwayRunAwaySpeedBonus;
        }

        public float GetSpeed()
        {
            return navigation.speed;
        }

        public bool CollectStick(Collider other)
        {
            if(stickInventory.collectingEnabled)
                return stickInventory.AddStick(other.transform);
            return false;
        }


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
            Vector3 randomDirection = Random.insideUnitSphere * randomSwimRadius;
            randomDirection += position;
            randomDirection.y = 0;
            
            // get closest point reachable from that position
            NavMesh.SamplePosition(randomDirection, out var hit, 3, 1);
            Vector3 finalPosition = hit.position;
 
            // check, if actually reachable
            NavMesh.CalculatePath(position, finalPosition, NavMesh.AllAreas, _path);
            if (_path.status == NavMeshPathStatus.PathComplete) 
            {
                Debug.Log ("Valid path has been found");
                return finalPosition;
            } 
            else 
            {
                Debug.Log("No path to that position, picking a new point");
                //return GetReachablePointInRadius();
                return Vector3.zero;
            }
        }

        #endregion
    }
}
