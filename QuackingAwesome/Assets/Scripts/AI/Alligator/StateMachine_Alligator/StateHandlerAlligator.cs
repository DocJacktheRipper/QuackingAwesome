using AI.Alligator.StateMachine_Alligator.ConcreteStates;
using AI.StateMachine;
using UnityEngine;

namespace AI.Alligator.StateMachine_Alligator
{
    public class StateHandlerAlligator : IStateHandlerAI
    {
        public BehaviourMethodsAlligator concreteMethods;
        
        #region Statemachine
        
        public IdleStateAlligator idle;
        public ChasingStateAlligator chasing;
        public SwimmingStateAlligator swimming;

        #endregion
        
        void Start()
        {
            Initialize(idle);
        }

        #region OverrideFunctions

        public override void BiteTriggerEntered(Collider other)
        {
            base.BiteTriggerEntered(other);
            concreteMethods.InvokeBiting();
        }

        public override void BiteTriggerExited(Collider other)
        {
            base.BiteTriggerExited(other);
        }

        #endregion
    }
}
