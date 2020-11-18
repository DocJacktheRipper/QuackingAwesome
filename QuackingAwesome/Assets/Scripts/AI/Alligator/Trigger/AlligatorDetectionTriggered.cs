using System;
using AI.Alligator.StateMachine_Alligator;
using AI.StateMachine;
using UnityEngine;

namespace AI.Alligator.Trigger
{
    public class AlligatorDetectionTriggered : MonoBehaviour
    {
        public StateHandlerAlligator stateHandler;

        private void OnTriggerEnter(Collider other)
        {
            stateHandler.DetectionTriggerEntered(other);
        }

        private void OnTriggerExit(Collider other)
        {
            stateHandler.DetectionTriggerExited(other);
        }
    }
}
