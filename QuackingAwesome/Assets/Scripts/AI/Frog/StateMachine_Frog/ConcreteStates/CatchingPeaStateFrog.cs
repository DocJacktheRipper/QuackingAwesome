using UnityEngine;

namespace AI.Frog.StateMachine_Frog.ConcreteStates
{
    public class CatchingPeaStateFrog : IStateFrog
    {
        public Transform peaPosition;

        #region IStateBeaver

        public override void Enter()
        {
            base.Enter();
            
            ConcreteMethods.Chase(peaPosition);
            ConcreteMethods.StartMovement();
        }

        public override void Execute()
        {
            base.Execute();
            
            if (ConcreteMethods.HasReachedDestination())
            {
                StateHandler.ChangeState(StateHandler.swimming);
            }
            if ((peaPosition == null))
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

            if (CheckPeaIsTrigger(other))
            {
                AnotherPeaDetected(other.transform);
            }
        }

        public override void MouthTriggerEntered(Collider other)
        {
            base.MouthTriggerEntered(other);

            if (CheckPeaIsTrigger(other))
            {
                //Debug.Log("Beaver has collected stick.");
                ConcreteMethods.EatPea(other);
                StateHandler.ChangeState(StateHandler.swimming);
            }
        }

        #endregion
        #region HelperMethods

        private void AnotherPeaDetected(Transform stickPositionNew)
        {
            // check if other stick is closer
            // [not necessary, because always further away?]
            var position = transform.position;
            var distanceVector = (this.peaPosition.position - position);
            var distanceNew = (stickPositionNew.position - position);
            
            if ( distanceNew.sqrMagnitude < distanceVector.sqrMagnitude )
            {
                this.peaPosition = stickPositionNew;
                StateHandler.ChangeState(StateHandler.catching);
            }
        }

        #endregion
    }
}
