using UnityEngine;

namespace AI.Alligator.States
{
    public class IdleState : IState
    {
        private float waitUntilToSwitch;
        private float waitSecondsToSwitch;
        private int chanceToSwitchToSwimming;

        public IdleState(GameObject ai) : base(ai) { }
        public IdleState(GameObject ai, float waitSeconds, int chance) : base(ai)
        {
            waitSecondsToSwitch = waitSeconds;
            chanceToSwitchToSwimming = chance;
        }

        #region IState
        public override void Enter()
        {
            base.Enter();
            waitUntilToSwitch = Time.time + waitSecondsToSwitch;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Execute()
        {
            if (SwitchStatesAfterTime())
            {
                //alligatorNavigation.isStopped = true;
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
