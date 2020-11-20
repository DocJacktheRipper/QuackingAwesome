using Nest;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class GoingHomeStateBeaver : IStateBeaver
    {
        public BeaverNest beaverNest;

        public override void Enter()
        {
            base.Enter();
            
            ConcreteMethods.SetDestination(beaverNest.transform.position);
            ConcreteMethods.StartMovement();
        }

        public override void Execute()
        {
            base.Execute();

            if (ConcreteMethods.HasReachedDestination())
            {
                StateHandler.ChangeState(StateHandler.idle);
            }
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            if (StickTriggeredDetection(other))
            {
                return;
            }
        }
    }
}
