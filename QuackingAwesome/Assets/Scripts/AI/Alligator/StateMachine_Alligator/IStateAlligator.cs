using AI.StateMachine;

namespace AI.Alligator.StateMachine_Alligator
{
    public class IStateAlligator : IState
    {
        protected StateHandlerAlligator StateHandler;
        protected BehaviourMethodsAlligator ConcreteMethods;

        protected override void Awake()
        {
            base.Awake();
            
            StateHandler = ai.GetComponent<StateHandlerAlligator>();

            ConcreteMethods = ai.GetComponent<BehaviourMethodsAlligator>();
        }
    }
}
