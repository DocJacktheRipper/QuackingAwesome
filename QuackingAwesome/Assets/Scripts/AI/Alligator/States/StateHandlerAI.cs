using System;
using UnityEngine;

namespace AI.Alligator.States
{
    public class StateHandlerAI : MonoBehaviour
    {
        public BehaviourMethods methods;

        #region StateMachine

        public IState state = null;

        public IdleState idle;
        public ChasingState chasing;

        #endregion

        #region Initialization

        void Start()
        {
            Initialize(idle);
        }

        public void Initialize(IState startingState)
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
            
        }

        public void BiteTriggerEntered(Collider other)
        {
            
        }

        public void BiteTriggerExited(Collider other)
        {
            
        }
        #endregion
    }
}
