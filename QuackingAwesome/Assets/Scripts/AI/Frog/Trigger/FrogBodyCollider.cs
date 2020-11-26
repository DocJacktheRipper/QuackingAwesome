using AI.Frog.StateMachine_Frog;
using UnityEngine;

namespace AI.Frog.Trigger
{
    public class FrogBodyCollider : MonoBehaviour
    {
        public StateHandlerFrog stateHandler;

        private void OnTriggerEnter(Collider other)
        {
            stateHandler.MouthTriggerEntered(other);
        }
    }
}
