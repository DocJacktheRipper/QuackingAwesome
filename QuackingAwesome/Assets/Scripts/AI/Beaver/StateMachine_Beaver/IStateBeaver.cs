using System;
using AI.StateMachine;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class IStateBeaver : IState
    {
        protected StateHandlerBeaver stateHandler;
        protected BehaviourMethodsBeaver concreteMethods;

        protected override void Awake()
        {
            base.Awake();
            
            stateHandler = ai.GetComponent<StateHandlerBeaver>();

            concreteMethods = ai.GetComponent<BehaviourMethodsBeaver>();
        }

        protected bool StickIsTrigger(Collider other)
        {
            if (other.CompareTag("Stick"))
            {
                stateHandler.fetching.stickPosition = other.transform;
                stateHandler.ChangeState(stateHandler.fetching);
                return true;
            }

            return false;
        }

        public override void MouthTriggerEntered(Collider other)
        {
            base.MouthTriggerEntered(other);
            
        }
        private bool StickCollectable(Collider stickCollider)
        {
            if (!stickCollider.CompareTag("Stick"))
            {
                return false;
            }
            // add Stick to beaver
            

            return true;
        }
    }
}
