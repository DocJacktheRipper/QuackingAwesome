using AI.Alligator.StateMachine_Alligator.ConcreteStates;
using AI.StateMachine;

namespace AI.Alligator.StateMachine_Alligator
{
    public class StateHandlerAlligator : IStateHandlerAI
    {
        #region Statemachine
        
        public IdleState idle;
        public ChasingState chasing;
        public SwimmingState swimming;

        #endregion
        
        void Start()
        {
            Initialize(idle);
        }
    }
}
