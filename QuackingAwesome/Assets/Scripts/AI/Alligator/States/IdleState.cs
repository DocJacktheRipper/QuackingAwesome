using UnityEngine;

namespace AI.Alligator.States
{
    public class IdleState : IState
    {
        private float waitUntil;
        private int chanceToSwitchToSwimming;
        
        public IdleState(float waitSeconds, int chance)
        {
            waitUntil = Time.time + waitSeconds;
            chanceToSwitchToSwimming = chance;
        }
        
        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            if (SwitchStatesAfterTime())
            {
                // TODO ?
            }
        }
        
        private bool SwitchStatesAfterTime()
        {
            if (waitUntil > Time.time)
            {
                var ranInt = Random.Range(0, 1);
                if (ranInt < chanceToSwitchToSwimming)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
