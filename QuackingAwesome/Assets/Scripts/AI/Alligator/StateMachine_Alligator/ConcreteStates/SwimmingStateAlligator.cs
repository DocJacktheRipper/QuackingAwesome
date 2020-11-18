using AI.StateMachine;
using UnityEngine;

namespace AI.Alligator.StateMachine_Alligator.ConcreteStates
{
    public class SwimmingStateAlligator : ISwimIdleStateAlligator
    {
        private static readonly int IsSwimming = Animator.StringToHash("IsSwimming");
        
        #region
        public override void Enter()
        {
            base.Enter();
            methods.StartMovement();
            methods.animator.SetBool(IsSwimming, true);
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

        public override void Exit()
        {
            base.Exit();
            
            methods.animator.SetBool(IsSwimming, false);
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);

            if (other.CompareTag("Player"))
            {
                stateHandler.chasing.target = other.transform;
                stateHandler.ChangeState(stateHandler.chasing);
            }
        }
        #endregion
    }
}