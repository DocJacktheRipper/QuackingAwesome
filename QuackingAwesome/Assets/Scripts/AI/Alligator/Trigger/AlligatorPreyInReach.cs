using System;
using AI.Alligator.StateMachine_Alligator;
using UnityEngine;

namespace AI.Alligator
{
    public class AlligatorPreyInReach : MonoBehaviour
    {
        public StateHandlerAlligator alligator;

        private void OnTriggerEnter(Collider other)
        {
            alligator.BiteTriggerEntered(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            alligator.BiteTriggerExited(other);
        }
    }
}
