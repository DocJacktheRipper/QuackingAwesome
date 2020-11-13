using AI.Alligator.StateMachine_Alligator.ConcreteStates;
using AI.StateMachine;

namespace AI.Alligator.StateMachine_Alligator
{
    public class StateHandlerAlligator : IStateHandlerAI
    {
        #region Statemachine
        
        public IdleStateAlligator idle;
        public ChasingStateAlligator chasing;
        public SwimmingStateAlligator swimming;

        #endregion
        
        void Start()
        {
            Initialize(idle);
        }
    }
}
