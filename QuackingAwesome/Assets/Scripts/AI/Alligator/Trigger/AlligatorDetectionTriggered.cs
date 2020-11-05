using System;
using AI.StateMachine;
using UnityEngine;

namespace AI.Alligator.Trigger
{
    public class AlligatorDetectionTriggered : MonoBehaviour
    {
        public IStateHandlerAI stateHandler;

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
