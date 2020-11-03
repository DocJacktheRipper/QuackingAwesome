using UnityEngine;

namespace AI.Alligator.States
{
    public class IdleState : IState
    {
        private float waitUntilToSwitch;
        private float waitSecondsToSwitch;
        private int chanceToSwitchToSwimming;

        private static readonly int IsIdle = Animator.StringToHash("IsIdle");

        public IdleState(GameObject ai, StateHandlerAI sh, float waitSeconds, int chance) : base(ai, sh)
        {
            waitSecondsToSwitch = waitSeconds;
            chanceToSwitchToSwimming = chance;
        }

        #region IState
        public override void Enter()
        {
            base.Enter();
            waitUntilToSwitch = Time.time + waitSecondsToSwitch;
            methods.animator.SetBool(IsIdle, true);
        }

        public override void Exit()
        {
            base.Exit();
            methods.animator.SetBool(IsIdle, false);
        }

        public override void Execute()
        {
            if (SwitchStatesAfterTime())
            {
                //alligatorNavigation.isStopped = true;
            }
        }

        public void DetectionTriggerEntered(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                stateHandler.chasing.target = other.transform;
                stateHandler.ChangeState(stateHandler.chasing);
            }
        }

        #endregion
        #region HelperMethods

        public void SetWaitingTime()
        {
            waitUntilToSwitch = Time.time + waitSecondsToSwitch;
        }

        private bool SwitchStatesAfterTime()
        {
            if (!(Time.time > waitUntilToSwitch)) return false;
            
            // try to switch after time elapsed
            var ranInt = Random.Range(0, 100);
                
            var status = " - time elapsed - changes: " + ranInt + "/" + chanceToSwitchToSwimming;
            if (ranInt < chanceToSwitchToSwimming)
            {
                status += " -- switched";
                Debug.Log(status);
                return true;
            }
            Debug.Log(status);

            return false;
        }

        #endregion
        
    }
}
