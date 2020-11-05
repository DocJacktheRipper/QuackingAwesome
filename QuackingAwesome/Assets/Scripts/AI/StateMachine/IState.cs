using UnityEngine;

namespace AI.Alligator.States
{
    public abstract class IState : MonoBehaviour
    {
        protected GameObject ai;

        protected BehaviourMethods methods;
        protected StateHandlerAI stateHandler;

        private void Awake()
        {
            ai = transform.parent.gameObject;

            methods = ai.GetComponent<BehaviourMethods>();
            if (methods == null)
                Debug.Log("Fault");
            stateHandler = ai.GetComponent<StateHandlerAI>();

            methods.GotoNextPoint();
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Execute() { }
        
        #region Trigger
        public virtual void DetectionTriggerEntered(Collider other) { }
        public virtual void DetectionTriggerExited(Collider other) { }
        #endregion
    }
}
