using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class SwimmingStateBeaver : ISwimIdleStateBeaver
    {
        //private float detectionRadius;
        //private Vector3 detectionCenter;
        
        #region
        public override void Enter()
        {
            base.Enter();
            methods.StartMovement();
        }
        
        public override void Execute()
        {
            base.Execute();
            if (base.SwitchStatesAfterTime())
            {
                ResetWaitingTime();
                StateHandler.ChangeState(StateHandler.idle);
                return;
            }
            if (methods.HasReachedDestination())
            {
                methods.GotoNextPoint();
            }
            
            //SearchForSticks();
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            
            if (StickIsTrigger(other))
            {
                return;
            }
        }
        #endregion

        
        /*
        private void SearchForSticks()
        {
            Collider[] hitColliders = Physics.OverlapSphere(detectionCenter, detectionRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Stick"))
                {
                    Debug.Log("Detected Stick!");
                    StateHandler.fetching.stickPosition = hitCollider.transform;
                    StateHandler.ChangeState(StateHandler.fetching);
                }
            }
            // TODO: swim away from alligator
        }
        */
    }
}
