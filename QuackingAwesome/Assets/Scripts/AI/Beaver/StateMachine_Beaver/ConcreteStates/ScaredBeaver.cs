using AI.Beaver.Trigger;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class ScaredBeaver : IStateBeaver
    {
        public ScaredAway scaredAway;
        private float _waitUntil;

        public override void Enter()
        {
            base.Enter();
            InvokeScared(scaredAway.source, scaredAway.howFarAway);
            concreteMethods.AddSpeed(scaredAway.runAwaySpeedBonus);

            // calculate when to switch to different state again
            _waitUntil = Time.time + (scaredAway.howFarAway / concreteMethods.GetSpeed());
            
            concreteMethods.StartMovement();
        }

        public override void Execute()
        {
            base.Execute();
            
            // End state after time...
            if (_waitUntil < Time.time)
            {
                stateHandler.ChangeState(stateHandler.swimming);
            }
        }

        /*
        public override void DetectionTriggerExited(Collider other)
        {
            base.DetectionTriggerExited(other);
            // if far away, it's not scared anymore
            Debug.Log("Tag exited: " + other.tag);
            if (other.CompareTag("Player") || other.CompareTag("Alligator"))
            {
                stateHandler.ChangeState(stateHandler.swimming);
            }
        }*/

        public override void Exit()
        {
            base.Exit();
            concreteMethods.AddSpeed(-scaredAway.runAwaySpeedBonus);
        }


        #region HelperMethods

        /// <summary>
        /// Change target to position opposite of given source with fixed distance to that.
        /// </summary>
        /// <param name="source">from where to go away</param>
        /// <param name="distance">how far away is the new target point</param>
        public void InvokeScared(Transform source, float distance)
        {
            var position = transform.position;
            var dir = (position - source.position);
            var relPoint = Vector3.Normalize(dir) * distance;
            var point = position + relPoint;

            concreteMethods.SetDestination(point);
        }

        #endregion
    }
}
