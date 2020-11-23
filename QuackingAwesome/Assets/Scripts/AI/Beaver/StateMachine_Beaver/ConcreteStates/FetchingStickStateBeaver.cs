using System.Data;
using Inventory;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class FetchingStickStateBeaver : IStateBeaver
    {
        public Transform stickPosition;
        public StickInventory inventory;

        #region IStateBeaver

        public override void Enter()
        {
            base.Enter();
            
            ConcreteMethods.Chase(stickPosition);
            ConcreteMethods.StartMovement();
        }

        public override void Execute()
        {
            base.Execute();
            if (!inventory.collectingEnabled || inventory.numberOfSticks >= inventory.maxCapacityOfSticks)
            {
                StateHandler.ChangeState(StateHandler.swimming);
            }

            if (ConcreteMethods.HasReachedDestination())
            {
                StateHandler.ChangeState(StateHandler.swimming);
            }
            if (!(stickPosition != null))
            {
                StateHandler.ChangeState(StateHandler.swimming);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            ConcreteMethods.GotoNextPoint();
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);

            if (base.StickIsTrigger(other))
            {
                AnotherStickDetected(other.transform);
            }
        }

        public override void MouthTriggerEntered(Collider other)
        {
            base.MouthTriggerEntered(other);

            if (StickIsTrigger(other))
            {
                //Debug.Log("Beaver has collected stick.");
                ConcreteMethods.CollectStick(other);
                StateHandler.ChangeState(StateHandler.goingHome);
            }
        }

        #endregion
        #region HelperMethods

        private void AnotherStickDetected(Transform stickPositionNew)
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

        #endregion
    }
}
