using UnityEngine;
using UnityEngine.AI;

namespace AI.StateMachine
{
    public class BehaviourMethods : MonoBehaviour
    {
        public NavMeshAgent navigation;
        
        public Transform wayPointContainer;
        public float randomSwimRadius;
        public Vector3 currentTarget;
        private NavMeshPath _path;

        public Animator animator;

        #region UsefulProperties
        public float chaseSpeedBonus;
        #endregion

       
        private void Start()
        {
            navigation = GetComponentInParent<NavMeshAgent>();
            _path = new NavMeshPath();
            GotoNextPoint();
        }

        #region Move
        public void StartMovement()
        {
            navigation.isStopped = false;
            navigation.SetDestination(currentTarget);
        }
        public void StopMovement()
        {
            navigation.isStopped = true;
        }

        public bool HasReachedDestination()
        {
            if (!navigation.pathPending && navigation.remainingDistance < 0.5f)
            {
                return true;
            }
            return false;
        }

        public void GotoNextPoint()
        {
            if (wayPointContainer.childCount <= 0)
            {
                return;
            }

            /*
            currentTarget = GetReachablePointInRadius();
            navigation.SetPath(_path);
            */

            // set new target by getting random point from container
            var destIndex = Random.Range(0, wayPointContainer.childCount);
            currentTarget = wayPointContainer.GetChild(destIndex).position;
            navigation.SetDestination(currentTarget);
        }
        
        public void SetDestination(Vector3 dest)
        {
            currentTarget = dest;
            navigation.SetDestination(dest);
        }

        private Vector3 GetReachablePointInRadius()
        {
            var position = transform.position;
            
            // calculate random position
            Vector3 randomDirection = Random.insideUnitSphere * randomSwimRadius;
            randomDirection.y = 0;
            randomDirection += position;
            
            // get closest point reachable from that position
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, randomSwimRadius, 1);
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
        #region Chase

        public void InvokeChasing()
        {
            // TODO: stuff
            navigation.speed += chaseSpeedBonus;
        }

        public void Chase(Transform target)
        {
            currentTarget = target.position;
            SetDestination(currentTarget);
        }

        public void StopChasing()
        {
            navigation.speed -= chaseSpeedBonus;
        }

        #endregion
        
    }
}
