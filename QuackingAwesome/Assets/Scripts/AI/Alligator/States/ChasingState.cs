﻿using UnityEngine;

namespace AI.Alligator.States
{
    public class ChasingState : IState
    {
        public Transform target;

        public override void Enter()
        {
            base.Enter();
            methods.InvokeChasing();
        }

        public override void Execute()
        {
            base.Execute();
            methods.Chase(target);
        }

        public override void Exit()
        {
            base.Exit();
            methods.StopChasing();
        }

        public override void DetectionTriggerEntered(Collider other)
        {
            base.DetectionTriggerEntered(other);
            // TODO: calculate which target to focus on
            // if(targetChanged)
            //    stateHandler.ChangeStates(stateHandler.chaseState);
        }
    }
}