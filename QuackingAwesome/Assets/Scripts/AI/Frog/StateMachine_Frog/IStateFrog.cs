using AI.StateMachine;
using UnityEngine;

namespace AI.Frog.StateMachine_Frog
{
    public class IStateFrog : IState
    {
        protected StateHandlerFrog StateHandler;
        protected BehaviourMethodsFrog ConcreteMethods;

        private void Awake()
        {
            base.Awake();
        
            StateHandler = ai.GetComponent<StateHandlerFrog>();
            ConcreteMethods = ai.GetComponent<BehaviourMethodsFrog>();
        }

        protected bool CheckPeaIsTrigger(Collider other)
        {
            return other.CompareTag("Pea");
        }
    }
}
