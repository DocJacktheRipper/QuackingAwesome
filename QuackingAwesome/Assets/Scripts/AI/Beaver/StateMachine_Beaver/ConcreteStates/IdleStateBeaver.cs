using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class IdleStateBeaver : ISwimIdleStateBeaver
    {
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");


        #region IState

        public override void Enter()
        {
            base.Enter();
            methods.StopMovement();
            //methods.animator.SetBool(IsIdle, true);
        }

        public override void Exit()
        {
            base.Exit();
            //methods.animator.SetBool(IsIdle, false);
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
            // TODO: swim away from alligator
        }

        #endregion
    }
}
