using AI.Alligator.StateMachine_Alligator.ConcreteStates;
using UnityEngine;

namespace AI.StateMachine
{
    public class IStateHandlerAI : MonoBehaviour
    {
        public BehaviourMethods methods;

        #region StateMachine

        public IState state = null;

        #endregion

        #region Initialization

        protected void Initialize(IState startingState)
        {
            state = startingState;
            startingState.Enter();
        }


        #endregion
        
        public void ChangeState(IState newState)
        {
            state.Exit();

            state = newState;
            newState.Enter();
        }

        public void Update()
        {
            state.Execute();
        }
        
        #region Trigger

        public void DetectionTriggerEntered(Collider other)
        {
            state.DetectionTriggerEntered(other);
        }
        public void DetectionTriggerExited(Collider other)
        {
            state.DetectionTriggerExited(other);
        }

        public virtual void BiteTriggerEntered(Collider other)
        {
        }
        public virtual void BiteTriggerExited(Collider other)
        {
        }

        public virtual void MouthTriggerEntered(Collider other)
        {
            state.MouthTriggerEntered(other);
        }
        
        #endregion
    }
}
