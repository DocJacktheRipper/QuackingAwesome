using UnityEngine;

namespace AI.Alligator.StateMachine_Alligator.ConcreteStates
{
    public class ScaredStateAlligator : IStateAlligator
    {
        public ScaredAway scaredAway;
        private float _waitUntil;
        public bool divingEnabled;
        public float respawnTime;

        public override void Enter()
        {
            base.Enter();
            InvokeScared(scaredAway.source, scaredAway.howFarAway);
            ConcreteMethods.AddSpeed(scaredAway.runAwaySpeedBonus);

            if (divingEnabled)
            {
                ConcreteMethods.DiveAndRespawnAtNest(respawnTime);
            }

            // calculate when to switch to different state again
            _waitUntil = Time.time + (scaredAway.howFarAway / ConcreteMethods.GetSpeed());
            
            ConcreteMethods.StartMovement();
        }

        public override void Execute()
        {
            base.Execute();
            
            // End state after time...
            if (_waitUntil < Time.time)
            {
                StateHandler.ChangeState(StateHandler.swimming);
            }
        }

        public override void Exit()
        {
            base.Exit();
            ConcreteMethods.AddSpeed(-scaredAway.runAwaySpeedBonus);
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

            ConcreteMethods.SetDestination(point);
        }

        #endregion
    }
}
