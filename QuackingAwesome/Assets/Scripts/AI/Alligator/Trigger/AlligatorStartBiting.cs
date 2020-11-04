using AI.Alligator.States;
using UnityEngine;

namespace AI.Alligator.Trigger
{
    public class AlligatorStartBiting : MonoBehaviour
    {
        public StateHandlerAI handler;

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
