using AI.StateMachine;
using UnityEngine;

namespace AI.Alligator.StateMachine_Alligator.ConcreteStates
{
    public class ChasingStateAlligator : IStateAlligator
    {
        public Transform target;

        public override void Enter()
        {
            base.Enter();
            methods.StartMovement();
            methods.InvokeChasing();
        }

        public override void Execute()
        {
            base.Execute();
            methods.Chase(target);
        }

        public override void Exit()
        {
            base.Exit();
            methods.StopChasing();
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            // TODO: calculate which target to focus on
            // if(targetChanged)
            //    stateHandler.ChangeStates(stateHandler.chaseState);
        }

        public override void DetectionTriggerExited(Collider other)
        {
            base.DetectionTriggerExited(other);

            if(other.transform.Equals(target))
            {
                StateHandler.ChangeState(StateHandler.idle);
            }
        }
    }
}