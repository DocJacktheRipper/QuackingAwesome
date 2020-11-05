using Assets.Scripts.AI.Alligator.States;
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
        public SwimmingState swimming;

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
