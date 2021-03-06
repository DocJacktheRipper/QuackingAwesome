﻿using AI.StateMachine;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class IStateBeaver : IState
    {
        protected StateHandlerBeaver StateHandler;
        protected BehaviourMethodsBeaver ConcreteMethods;

        protected override void Awake()
        {
            base.Awake();
            
            StateHandler = ai.GetComponent<StateHandlerBeaver>();
            ConcreteMethods = ai.GetComponent<BehaviourMethodsBeaver>();
        }

        protected bool StickTriggeredDetection(Collider other)
        {
            if (StickIsTrigger(other))
            {
                //Debug.Log("Yes, beaver has seen stick.");
                StateHandler.StickDetected(other.transform);
                return true;
            }

            return false;
        }

        protected bool StickIsTrigger(Collider other)
        {
            return other.CompareTag("Stick");
        }
    }
}
