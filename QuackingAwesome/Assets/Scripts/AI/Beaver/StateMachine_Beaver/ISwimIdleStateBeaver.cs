using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class ISwimIdleStateBeaver : IStateBeaver
    {
        public float waitUntilToSwitch;
        public float waitSecondsToSwitch;
        public int chanceToSwitchState;

        #region overridden
        public override void Enter()
        {
            base.Enter();
            waitUntilToSwitch = Time.time + waitSecondsToSwitch;
        }

        #endregion

        protected void ResetWaitingTime()
        {
            waitUntilToSwitch = Time.time + waitSecondsToSwitch;
        }

        protected bool SwitchStatesAfterTime()
        {
            if (!(Time.time > waitUntilToSwitch)) return false;

            // try to switch after time elapsed
            var ranInt = Random.Range(0, 100);

            if (ranInt < chanceToSwitchState)
            {
                return true;
            }

            return false;
        }
    }
}
