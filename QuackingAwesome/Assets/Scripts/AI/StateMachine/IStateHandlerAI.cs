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

        public void BiteTriggerEntered(Collider other)
        {
            methods.InvokeBiting();
        }

        public void BiteTriggerExited(Collider other)
        {
            methods.StopBiting();
        }
        
        #endregion
    }
}
