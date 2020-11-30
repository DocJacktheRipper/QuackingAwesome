using AI.StateMachine;
using UnityEngine;

namespace AI.Alligator.StateMachine_Alligator.ConcreteStates
{
    public class IdleStateAlligator : ISwimIdleStateAlligator
    {
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");

        
        #region IState
        public override void Enter()
        {
            base.Enter();
            methods.StopMovement();
            methods.animator.SetBool(IsIdle, true);
        }

        public override void Exit()
        {
            base.Exit();
            methods.animator.SetBool(IsIdle, false);
        }

        public override void Execute()
        {
            base.Execute();
            if (base.SwitchStatesAfterTime())
            {
                ResetWaitingTime();
                StateHandler.ChangeState(StateHandler.swimming);
            }
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            if (other.CompareTag("Player"))
            {
                StateHandler.chasing.target = other.transform;
                StateHandler.ChangeState(StateHandler.chasing);
            }
        }
        #endregion

    }
}
