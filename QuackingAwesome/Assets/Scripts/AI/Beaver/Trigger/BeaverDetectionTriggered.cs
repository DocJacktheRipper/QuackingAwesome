using AI.Beaver.StateMachine_Beaver;
using UnityEngine;

namespace AI.Beaver.Trigger
{
    public class BeaverDetectionTriggered : MonoBehaviour
    {
        public StateHandlerBeaver stateHandler;
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
