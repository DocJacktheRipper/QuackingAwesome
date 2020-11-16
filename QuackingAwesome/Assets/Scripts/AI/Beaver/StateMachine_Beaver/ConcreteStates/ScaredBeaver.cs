

using System.Runtime.Remoting.Services;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver.ConcreteStates
{
    public class ScaredBeaver : IStateBeaver
    {
        public ScaredAway scaredAway;

        public override void Enter()
        {
            base.Enter();
            InvokeScared(scaredAway.source, scaredAway.howFarAway);
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
