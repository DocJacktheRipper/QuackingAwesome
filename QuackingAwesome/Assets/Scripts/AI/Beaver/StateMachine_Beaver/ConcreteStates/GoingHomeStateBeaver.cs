using Nest;
using Nest.NPC_Nests;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class GoingHomeStateBeaver : IStateBeaver
    {
        public BeaverNest beaverNest;

        #region IState

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
                
                // reset timer, so beaver is not tired anymore
                StateHandler.swimming.ResetTimer();
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

        public override void MouthTriggerEntered(Collider other)
        {
            base.MouthTriggerEntered(other);
            if(BeaverNestIsTrigger(other))
            {
                Debug.Log("Sticks: " + ConcreteMethods.stickInventory.numberOfSticks);
                return;
            }
        }

        #endregion

        #region HelperMethods

        private bool BeaverNestIsTrigger(Collider other)
        {
            if (!other.CompareTag("BeaverNest"))
            {
                return false;
            }

            var nest = other.GetComponent<BeaverNest>();
            var n = nest.TransferSticksToNest(ConcreteMethods.stickInventory);
            return true;
        }

        #endregion
    }
}
