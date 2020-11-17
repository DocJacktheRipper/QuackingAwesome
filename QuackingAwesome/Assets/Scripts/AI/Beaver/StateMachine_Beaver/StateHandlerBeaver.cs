using AI.Beaver.StateMachine_Beaver.ConcreteStates;
using AI.StateMachine;
using UnityEditor.VersionControl;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class StateHandlerBeaver : IStateHandlerAI
    {
        #region Statemachine
        
        public IdleStateBeaver idle;
        public SwimmingStateBeaver swimming;
        public FetchingStickBeaver fetching;
        public GoingHomeBeaver goingHome;
        public ScaredBeaver scared;

        #endregion
        
        void Start()
        {
            Initialize(idle);
        }
        
        public void StickCollected()
        {
            if (state.Equals(fetching))
            {
                ChangeState(goingHome);
            }
        }

        public void StickDetected(Transform stickTransform)
        {
            fetching.stickPosition = stickTransform;
            ChangeState(fetching);
        }
    }
}
