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

        public AlligatorState currentState = AlligatorState.Swimming;
        private NavMeshAgent alligatorNavigation;
        
        public Transform[] wayPoints;
        public Transform currentTarget;
        public float chaseSpeedBonus;

        public int chanceToSwitchStates = 50;
        public float waitForSeconds_Idle;
        public float waitForSeconds_Swimming;
        private float lastSwitchTime;

        private GameObject duck;
        
        // animations
        public Animator animator;
        private static readonly int DoBite = Animator.StringToHash("DoBite");
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");


        private void Start()
        {
            alligatorNavigation = GetComponentInParent<NavMeshAgent>();
            duck = GameObject.Find("Duck");
            
            lastSwitchTime = Time.time;
            currentState = AlligatorState.Idle;
            GotoNextPoint();
            
            animator.SetBool(IsIdle, true);
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
                    if (SwitchStatesAfterTime(waitForSeconds_Idle))
                    {
                        lastSwitchTime = Time.time;
                        currentState = AlligatorState.Swimming;
                        animator.SetBool(IsIdle, true);
                    }
                    break;
                case AlligatorState.Swimming:
                    Swim();
                    // switch between idle and swimming after some time
                    if (SwitchStatesAfterTime(waitForSeconds_Swimming))
                    {
                        lastSwitchTime = Time.time;
                        currentState = AlligatorState.Idle;
                        animator.SetBool(IsIdle, true);
                    }
                    break;
                case AlligatorState.Chasing:
                    Chase();
                    // TODO: on state enter etc.
                    // animator.SetBool(IsIdle, true);
                    break;
                case AlligatorState.Attacking:
                    Attack();
                    
                    animator.SetBool(IsIdle, false);
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
            alligatorNavigation.isStopped = false;
            alligatorNavigation.destination = duck.transform.position;
        }

        private void Attack()
        {
            
        }

        private void Swim()
        {
            alligatorNavigation.isStopped = false;
            if (!alligatorNavigation.pathPending && alligatorNavigation.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }    
        }

        private void Idle()
        {
            // do nothing or animation?
            alligatorNavigation.isStopped = true;
        }
        
        private void GotoNextPoint()
        {
            if (wayPoints.Length == 0)
            {
                return;
            }
            var destPoint = Random.Range(0, wayPoints.Length);
            alligatorNavigation.SetDestination(wayPoints[destPoint].position);
        }
        
        private bool SwitchStatesAfterTime(float minDiff)
        {
            string status = "try changing states";
            if ( Time.time > (lastSwitchTime + minDiff) )
            {
                var ranInt = Random.Range(0, 100);

                status += " - time elapsed - changes: " + ranInt + "/" + chanceToSwitchStates;
                if (ranInt < chanceToSwitchStates)
                {
                    status += " -- switch";
                    Debug.Log(status);
                    return true;
                }
                Debug.Log(status);
            }
            Debug.Log(status);
            return false;
        }
        
        //**** External Access ****//
        public void Bite()
        {
            animator.SetTrigger(DoBite);
            currentState = AlligatorState.Attacking;
            alligatorNavigation.speed += 0.2f;
        }

        public void ExitBite()
        {
            animator.ResetTrigger(DoBite);
            currentState = AlligatorState.Chasing;
            alligatorNavigation.speed -= 0.2f;
        }
    }
}
