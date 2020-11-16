using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class SwimmingStateBeaver : ISwimIdleStateBeaver
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
                return;
            }
            if (methods.HasReachedDestination())
            {
                methods.GotoNextPoint();
            }
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            
            if (StickIsTrigger(other))
            {
                return;
            }
            // TODO: swim away from alligator
        }
        #endregion
    }
}
