using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI.Alligator
{
    public class BehaviourAlligator : MonoBehaviour
    {
        public enum AlligatorState
        {
            Idle = 0,
            Swimming = 1,
            Chasing = 2,
            Attacking = 3
        };

        public AlligatorState currentState;
        private NavMeshAgent alligatorNavigation;
        
        public Transform[] wayPoints;
        public Transform currentTarget;
        public float chaseSpeedBonus;

        public int chanceToSwitchStates = 50;
        public float waitMinimumSeconds = 0.5f;
        private float lastSwitchTime;

        private GameObject duck;


        private void Start()
        {
            alligatorNavigation = GetComponentInParent<NavMeshAgent>();
            duck = GameObject.Find("Duck");
            
            lastSwitchTime = Time.time;
            currentState = AlligatorState.Idle;
        }

        private void Update()
        {
            Behaviour();
        }

        private void Behaviour()
        {
            switch (currentState)
            {
                case AlligatorState.Idle:
                    Idle();
                    // switch between idle and swimming after some time
                    if (SwitchStatesAfterTime(waitMinimumSeconds))
                    {
                        lastSwitchTime = Time.time;
                        currentState = AlligatorState.Swimming;
                    }
                    break;
                case AlligatorState.Swimming:
                    Swim();
                    
                    if (SwitchStatesAfterTime(waitMinimumSeconds))
                    {
                        lastSwitchTime = Time.time;
                        currentState = AlligatorState.Idle;
                    }
                    break;
                case AlligatorState.Chasing:
                    Chase();
                    break;
                case AlligatorState.Attacking:
                    Attack();
                    break;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Beaver"))
            {
                currentState = AlligatorState.Chasing;
                currentTarget = other.transform;
                alligatorNavigation.speed += chaseSpeedBonus;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (currentState != AlligatorState.Chasing) return;
            if (!other.gameObject.Equals(currentTarget.gameObject)) return;
            
            // if chased target leaves detection range,
            // switch back to idle
            currentState = AlligatorState.Idle;
            GotoNextPoint();
            alligatorNavigation.speed -= chaseSpeedBonus;
        }

        private void Chase()
        {
            alligatorNavigation.destination = duck.transform.position;
        }

        private void Attack()
        {
            
        }

        private void Swim()
        {
            if (!alligatorNavigation.pathPending && alligatorNavigation.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }    
        }

        private void Idle()
        {
            // do nothing or animation?
        }
        
        private void GotoNextPoint()
        {
            if (wayPoints.Length == 0)
            {
                return;
            }
            var destPoint = Random.Range(0, wayPoints.Length);
            alligatorNavigation.destination = wayPoints[destPoint].position;
        }
        
        private bool SwitchStatesAfterTime(float minDiff)
        {
            if ((lastSwitchTime + minDiff) > Time.time)
            {
                var ranInt = Random.Range(0, 1);
                if (ranInt < chanceToSwitchStates)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
