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
                StateHandler.ChangeState(StateHandler.idle);
                return;
            }
            if (methods.HasReachedDestination())
            {
                methods.GotoNextPoint();
            }
            
            //SearchForSticks();
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            
            if (StickIsTrigger(other))
            {
                return;
            }
        }
        #endregion
    }
}
