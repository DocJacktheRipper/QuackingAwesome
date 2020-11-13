﻿using UnityEngine;
using UnityEngine.AI;

namespace AI.StateMachine
{
    public class BehaviourMethods : MonoBehaviour
    {
        public NavMeshAgent navigation;
        
        public Transform wayPointContainer;
        public Vector3 currentTarget;

        public Animator animator;

        #region UsefulProperties
        public float chaseSpeedBonus;
        #endregion

       
        private void Start()
        {
            navigation = GetComponentInParent<NavMeshAgent>();
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
            // set new target by getting random point from container
            var destIndex = Random.Range(0, wayPointContainer.childCount);
            currentTarget = wayPointContainer.GetChild(destIndex).position;
            navigation.SetDestination(currentTarget);
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
            navigation.SetDestination(currentTarget);
        }

        public void StopChasing()
        {
            navigation.speed -= chaseSpeedBonus;
        }

        #endregion
        
    }
}
