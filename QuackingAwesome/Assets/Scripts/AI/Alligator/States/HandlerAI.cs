using System;
using UnityEngine;

namespace AI.Alligator.States
{
    public class HandlerAI : MonoBehaviour
    {
        private IState state = null;

        private void Start()
        {
            Initialize(gameObject.AddComponent<IdleState>());
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

        public void Execute()
        {
            state.Execute();
        }
    }
}
