﻿using Nest;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class GoingHomeBeaver : IStateBeaver
    {
        public BeaverNest beaverNest;

        public override void Enter()
        {
            base.Enter();
            
            concreteMethods.SetDestination(beaverNest.transform.position);
            concreteMethods.StartMovement();
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            if (StickIsTrigger(other))
            {
                return;
            }
        }
    }
}
