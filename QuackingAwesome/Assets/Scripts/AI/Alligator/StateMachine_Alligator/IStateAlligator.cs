using AI.StateMachine;

namespace AI.Alligator.StateMachine_Alligator
{
    public class IStateAlligator : IState
    {
        protected StateHandlerAlligator stateHandler;

        protected override void Awake()
        {
            base.Awake();
            
            stateHandler = ai.GetComponent<StateHandlerAlligator>();
        }
    }
}
