using Inventory;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class FetchingStickBeaver : IStateBeaver
    {
        public Transform stickPosition;
        public StickInventory inventory;

        #region IStateBeaver

        public override void Enter()
        {
            base.Enter();
            
            methods.Chase(stickPosition);
            methods.StartMovement();
        }

        public override void Execute()
        {
            base.Execute();
            if (!inventory.collectingEnabled || inventory.numberOfSticks >= inventory.maxCapacityOfSticks)
            {
                StateHandler.ChangeState(StateHandler.swimming);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            methods.GotoNextPoint();
        }

        private void StickDetected(Transform stickPositionNew)
        {
            // check if other stick is closer
            // [not necessary, because always further away?]
            var position = transform.position;
            var distanceVector = (this.stickPosition.position - position);
            var distanceNew = (stickPositionNew.position - position);
            
            if ( distanceNew.sqrMagnitude < distanceVector.sqrMagnitude )
            {
                this.stickPosition = stickPositionNew;
                StateHandler.ChangeState(StateHandler.fetching);
            }
        }
        
        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);

            if (StickIsTrigger(other))
            {
                StickDetected(other.transform);
            }
        }

        #endregion
    }
}
