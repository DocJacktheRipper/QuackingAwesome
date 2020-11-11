using AI.StateMachine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class IStateBeaver : IState
    {
        protected StateHandlerBeaver stateHandler;
        protected BehaviourMethodsBeaver concreteMethods;

        protected override void Awake()
        {
            base.Awake();
            
            stateHandler = ai.GetComponent<StateHandlerBeaver>();

            concreteMethods = ai.GetComponent<BehaviourMethodsBeaver>();
        }
    }
}
