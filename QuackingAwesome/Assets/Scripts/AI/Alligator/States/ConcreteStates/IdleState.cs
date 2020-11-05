using Assets.Scripts.AI.Alligator.States;
using UnityEngine;

namespace AI.Alligator.States
{
    public class IdleState : ISwimIdleState
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
                stateHandler.ChangeState(stateHandler.swimming);
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
