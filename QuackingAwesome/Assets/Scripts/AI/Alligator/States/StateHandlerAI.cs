using Assets.Scripts.AI.Alligator.States;
using System;
using UnityEngine;

namespace AI.Alligator.States
{
    public class StateHandlerAI : MonoBehaviour
    {
        public BehaviourMethods methods;

        #region StateMachine

        private IState state = null;

        public IdleState idle;
        public ChasingState chasing;

        #endregion
        #region InitialProperties

        public float waitSecondsForSwitchingStates;
        public int chanceForSwitchingStates;

        #endregion

        void Start()
        {
            idle = new IdleState(gameObject, this, waitSecondsForSwitchingStates, chanceForSwitchingStates);
            chasing = new ChasingState(gameObject, this);

            Initialize(idle);
        }

        public void Initialize(IState startingState)
        {
            state = startingState;
            startingState.Enter();
        }

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

        public void TriggerEntered(Collider other)
        {
            state.DetectionTriggerEntered(other);
        }
        public void TriggerExited(Collider other)
        {
            
        }
    }
}
