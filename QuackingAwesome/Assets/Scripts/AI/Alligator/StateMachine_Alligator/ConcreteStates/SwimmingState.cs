using UnityEngine;

namespace AI.Alligator.StateMachine_Alligator.ConcreteStates
{
    public class SwimmingState : ISwimIdleState
    {
        #region
        public override void Enter()
        {
            base.Enter();
            methods.StartMovement();
        }
        
        public override void Execute()
        {
            base.Execute();
            if (base.SwitchStatesAfterTime())
            {
                ResetWaitingTime();
                stateHandler.ChangeState(stateHandler.idle);
            }
            if (methods.HasReachedDestination())
            {
                methods.GotoNextPoint();
            }
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);

            if (other.CompareTag("Player"))
            {
                stateHandler.ChangeState(stateHandler.chasing);
            }
        }
        #endregion
    }
}