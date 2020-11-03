using AI.Alligator.States;
using UnityEngine;

namespace Assets.Scripts.AI.Alligator.States
{
    public class ChasingState : IState
    {
        public Transform target;

        #region constructor
        public ChasingState(GameObject ai, StateHandlerAI sh) : base(ai, sh)
        {

        }
        #endregion

        public override void Enter()
        {
            base.Enter();
            methods.InvokeChasing(target);
        }

        public override void Execute()
        {
            base.Execute();
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
            //      methods.StopChasing();
            //      methods.InvokeChasing(target);
        }
    }
}