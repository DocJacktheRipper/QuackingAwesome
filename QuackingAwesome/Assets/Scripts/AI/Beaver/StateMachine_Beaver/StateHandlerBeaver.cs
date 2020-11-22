using AI.Beaver.StateMachine_Beaver.ConcreteStates;
using AI.StateMachine;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class StateHandlerBeaver : IStateHandlerAI
    {
        #region Statemachine
        
        public IdleStateBeaver idle;
        public SwimmingStateBeaver swimming;
        public FetchingStickStateBeaver fetching;
        public GoingHomeStateBeaver goingHome;
        public ScaredStateBeaver scared;

        #endregion
        
        void Start()
        {
            Initialize(idle);
        }
        
        public void StickCollected()
        {
            ChangeState(goingHome);
        }

        public void StickDetected(Transform stickTransform)
        {
            fetching.stickPosition = stickTransform;
            ChangeState(fetching);
        }
    }
}
