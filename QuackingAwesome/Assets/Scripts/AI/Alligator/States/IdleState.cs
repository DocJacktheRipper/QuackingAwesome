using UnityEngine;

namespace AI.Alligator.States
{
    public class IdleState : IState
    {
        public float waitUntilToSwitch;
        public float waitSecondsToSwitch;
        public int chanceToSwitchToSwimming;

        private static readonly int IsIdle = Animator.StringToHash("IsIdle");

        
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
                SetWaitingTime();
            }
        }

        public override void DetectionTriggerEntered(Collider other)
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
