using UnityEngine;
using UnityEngine.AI;

namespace AI.Alligator.States
{
    public class BehaviourMethods : MonoBehaviour
    {
        public NavMeshAgent alligatorNavigation;
        
        public Transform wayPointContainer;
        public Transform currentTarget;

        public Animator animator;

        #region UsefulProperties
        public float chaseSpeedBonus;
        public float biteSpeedBonus;
        #endregion

        private void Start()
        {
            alligatorNavigation = GetComponentInParent<NavMeshAgent>();
        }

        public void GotoNextPoint()
        {
            if (wayPointContainer.childCount <= 0)
            {
                return;
            }
            // set new target by getting random point from container
            var destIndex = Random.Range(0, wayPointContainer.childCount);
            currentTarget = wayPointContainer.GetChild(destIndex);
            alligatorNavigation.SetDestination(currentTarget.position);
        }

        public void InvokeChasing()
        {
            // TODO: stuff
            alligatorNavigation.speed += chaseSpeedBonus;
        }

        public void Chase(Transform target)
        {
            alligatorNavigation.SetDestination(target.position);
        }

        public void StopChasing()
        {
            alligatorNavigation.speed -= chaseSpeedBonus;
        }
    }
}
