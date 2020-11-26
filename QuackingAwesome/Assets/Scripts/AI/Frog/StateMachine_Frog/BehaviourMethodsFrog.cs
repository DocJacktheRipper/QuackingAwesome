using AI.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Frog.StateMachine_Frog
{
    public class BehaviourMethodsFrog : BehaviourMethods
    {
        private NavMeshPath _path;
        
        public void EatPea(Collider pea)
        {
            Destroy(pea.gameObject);
            // TODO: respawn it after some time
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
