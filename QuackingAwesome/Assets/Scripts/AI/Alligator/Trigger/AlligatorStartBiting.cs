using AI.Alligator.States;
using UnityEngine;

public class AlligatorStartBiting : MonoBehaviour
{
    public StateHandlerAI handler;

    private void OnTriggerEnter(Collider other)
    {
        handler.TriggerEntered(other);
    }

    private void OnTriggerExit(Collider other)
    {
        handler.TriggerExited(other);
    }
}
