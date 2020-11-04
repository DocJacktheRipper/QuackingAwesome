using System;
using AI.Alligator.States;
using UnityEngine;

namespace AI.Alligator.Trigger
{
    public class AlligatorDetectionTriggered : MonoBehaviour
    {
        public StateHandlerAI stateHandler;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("detected");
            stateHandler.DetectionTriggerEntered(other);
        }

        private void OnTriggerExit(Collider other)
        {
            stateHandler.DetectionTriggerExited(other);
        }
    }
}
