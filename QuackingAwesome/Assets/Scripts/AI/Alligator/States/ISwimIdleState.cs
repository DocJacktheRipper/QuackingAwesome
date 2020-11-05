using AI.Alligator.States;
using UnityEngine;

namespace Assets.Scripts.AI.Alligator.States
{
    public class ISwimIdleState : IState
    {
        public float waitUntilToSwitch;
        public float waitSecondsToSwitch;
        public int chanceToSwitchState;

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
            
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                stateHandler.chasing.target = other.transform;
            }
        }

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