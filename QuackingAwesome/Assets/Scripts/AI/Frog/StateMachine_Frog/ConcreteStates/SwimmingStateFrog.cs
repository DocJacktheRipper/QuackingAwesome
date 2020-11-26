using UnityEngine;

namespace AI.Frog.StateMachine_Frog.ConcreteStates
{
    public class SwimmingStateFrog : IStateFrog
    {
        #region StateHandler

        public override void Enter()
        {
            base.Enter();
            ConcreteMethods.StartMovement();
        }
        
        public override void Execute()
        {
            base.Execute();
            
            if (ConcreteMethods.HasReachedDestination())
            {
                ConcreteMethods.GotoNextPoint();
            }
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);

            if (CheckPeaIsTrigger(other))
            {
                StateHandler.PeaDetected(other.transform);
                return;
            }
        }

        #endregion
    }
}
