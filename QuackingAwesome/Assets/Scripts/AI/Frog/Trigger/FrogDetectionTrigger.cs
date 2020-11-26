using AI.Frog.StateMachine_Frog;
using UnityEngine;

namespace AI.Frog.Trigger
{
    public class FrogDetectionTrigger : MonoBehaviour
    {
        public StateHandlerFrog stateHandler;
    
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
