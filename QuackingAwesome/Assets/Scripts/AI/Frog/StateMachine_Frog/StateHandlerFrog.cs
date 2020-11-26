using AI.Frog.StateMachine_Frog.ConcreteStates;
using AI.StateMachine;
using UnityEngine;

namespace AI.Frog.StateMachine_Frog
{
    public class StateHandlerFrog : IStateHandlerAI
    {
        #region Statemachine

        public SwimmingStateFrog swimming;
        public CatchingPeaStateFrog catching;
        public ScaredStateFrog scared;

        #endregion

        private void Start()
        {
            Initialize(swimming);
        }

        public void PeaDetected(Transform peaTransform)
        {
            catching.peaPosition = peaTransform;
            ChangeState(catching);
        }
    }
}
