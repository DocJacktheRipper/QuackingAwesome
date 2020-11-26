using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class SwimmingStateBeaver : ISwimIdleStateBeaver
    {
        //private float detectionRadius;
        //private Vector3 detectionCenter;
        
        // after swimming for too long,
        // beaver needs some rest in its base
        public float possibleTimeOutsideNest;
        private float _goHomeAfter;

        private void Start()
        {
            ResetTimer();
        }

        #region StateHandler
        public override void Enter()
        {
            base.Enter();
            ConcreteMethods.StartMovement();
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
            
            if (ConcreteMethods.HasReachedDestination())
            {
                ConcreteMethods.GotoNextPoint();
            }

            if (TimeIsUp())
            {
                Debug.Log("Beaver is tired. Beaver is going home.");
                StateHandler.ChangeState(StateHandler.goingHome);
                return;
            }
            
            //SearchForSticks();
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            
            if (StickTriggeredDetection(other))
            {
                return;
            }
        }
        #endregion

        public void ResetTimer()
        {
            _goHomeAfter = Time.time + possibleTimeOutsideNest;
        }

        private bool TimeIsUp()
        {
            return _goHomeAfter < Time.time;
        }
        
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
