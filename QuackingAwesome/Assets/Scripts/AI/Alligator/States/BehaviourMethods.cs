using UnityEngine;
using UnityEngine.AI;

namespace AI.Alligator.States
{
    public class BehaviourMethods : MonoBehaviour
    {
        public NavMeshAgent alligatorNavigation;
        
        public Transform wayPointContainer;
        public Vector3 currentTarget;

        public Animator animator;

        #region UsefulProperties
        public float chaseSpeedBonus;
        public float biteSpeedBonus;
        #endregion

       
        private void Start()
        {
            alligatorNavigation = GetComponentInParent<NavMeshAgent>();
        }

        #region Move
        public void StartMovement()
        {
            alligatorNavigation.isStopped = false;
            alligatorNavigation.SetDestination(currentTarget);
        }
        public void StopMovement()
        {
            alligatorNavigation.isStopped = true;
        }

        public bool HasReachedDestination()
        {
            if (!alligatorNavigation.pathPending && alligatorNavigation.remainingDistance < 0.5f)
            {
                return true;
            }
            return false;
        }

        public void GotoNextPoint()
        {
            if (wayPointContainer.childCount <= 0)
            {
                Debug.Log("no child");
                return;
            }
            // set new target by getting random point from container
            var destIndex = Random.Range(0, wayPointContainer.childCount);
            currentTarget = wayPointContainer.GetChild(destIndex).position;
            Debug.Log("target: " + currentTarget.ToString());
            alligatorNavigation.SetDestination(currentTarget);
        }

        #endregion
        #region Chase

        public void InvokeChasing()
        {
            // TODO: stuff
            alligatorNavigation.speed += chaseSpeedBonus;
        }

        public void Chase(Transform target)
        {
            currentTarget = target.position;
            alligatorNavigation.SetDestination(currentTarget);
        }

        public void StopChasing()
        {
            alligatorNavigation.speed -= chaseSpeedBonus;
        }

        #endregion
        #region Bite

        public void InvokeBiting()
        {
            alligatorNavigation.speed += biteSpeedBonus;
        }

        public void StopBiting()
        {
            alligatorNavigation.speed -= biteSpeedBonus;
        }
        #endregion
    }
}
