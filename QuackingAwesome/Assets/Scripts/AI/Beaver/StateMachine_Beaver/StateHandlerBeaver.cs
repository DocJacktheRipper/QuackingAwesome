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
        public FetchingStickBeaver fetching;
        public GoingHomeBeaver goingHome;
        public ScaredBeaver scared;

        #endregion
        
        void Start()
        {
            Initialize(idle);
        }

        public void MouthColliderEntered(Collision other)
        {
            throw new System.NotImplementedException();
        }

        public void StickCollected()
        {
            if (state.Equals(fetching))
            {
                ChangeState(goingHome);
            }
        }
    }
}
