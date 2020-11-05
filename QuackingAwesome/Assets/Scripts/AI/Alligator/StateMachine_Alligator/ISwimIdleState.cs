using UnityEngine;

namespace AI.Alligator.StateMachine_Alligator
{
    public class ISwimIdleState : IStateAlligator
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

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            if (other.CompareTag("Player"))
            {
                stateHandler.chasing.target = other.transform;
            }
        }
        #endregion
        
        public void ResetWaitingTime()
        {
            waitUntilToSwitch = Time.time + waitSecondsToSwitch;
        }

        public bool SwitchStatesAfterTime()
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