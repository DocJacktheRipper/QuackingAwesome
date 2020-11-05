using AI.StateMachine;
using UnityEngine;

namespace AI.Alligator.Trigger
{
    public class AlligatorStartBiting : MonoBehaviour
    {
        public IStateHandlerAI handler;

        private void OnTriggerEnter(Collider other)
        {
            handler.BiteTriggerEntered(other);
        }

        private void OnTriggerExit(Collider other)
        {
            handler.BiteTriggerExited(other);
        }
    }
}
