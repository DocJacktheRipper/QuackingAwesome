using UnityEngine;

namespace AI.Alligator.States
{
    public abstract class IState : MonoBehaviour
    {
        protected GameObject ai;

        public BehaviourMethods methods;
        public StateHandlerAI stateHandler;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Execute() { }

        public virtual void DetectionTriggerEntered(Collider other) { }
    }
}
