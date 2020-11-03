using UnityEngine;

namespace AI.Alligator.States
{
    public abstract class IState : MonoBehaviour
    {
        protected GameObject ai;

        protected BehaviourMethods methods;
        protected StateHandlerAI stateHandler;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Execute() { }

        public virtual void DetectionTriggerEntered(Collider other) { }


        public IState(GameObject ai, StateHandlerAI stateHandler)
        {
            this.ai = ai;
            this.stateHandler = stateHandler;
        }
    }
}
