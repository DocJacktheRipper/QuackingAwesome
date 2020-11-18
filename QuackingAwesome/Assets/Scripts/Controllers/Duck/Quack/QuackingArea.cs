using AI.Beaver.StateMachine_Beaver;
using AI.Beaver.Trigger;
using UnityEngine;

namespace Controllers.Duck.Quack
{
    public class QuackingArea : MonoBehaviour
    {
        // How heavy is the quack? "damage"
        public float volume;
        private Collider _quackArea;

        private void Start()
        {
            _quackArea = GetComponent<Collider>();
        }

        private void OnTriggerStay(Collider other)
        {
            GameObject beaver;
            if ( (beaver = other.gameObject).CompareTag("Beaver"))
            {
                QuackToBeaver(beaver);
            }
        }

        private void QuackToBeaver(GameObject beaver)
        {
            var ai = beaver.transform.parent.Find("AI");
            if (ai == null)
            {
                Debug.Log("AI not found!");
                return;
            }
            ScaredAway scared = ai.Find("Scare").GetComponent<ScaredAway>();

            if (scared.AttemptScare(volume))
            {
                Debug.Log("Beaver is scared away");
                var stateHandler = ai.GetComponent<StateHandlerBeaver>();
                
                // prepare and change states
                scared.source = transform;
                stateHandler.ChangeState(stateHandler.scared);
            }

            _quackArea.enabled = false;
        }
        
        /* OLD ONE 
        private void QuackToBeaver()
        {
            if (_beaver.GetComponent<ScaredAway>().AttemptScare(volume))
            {
                Debug.Log("is scared away");
                float dist = _beaver.GetComponent<ScaredAway>().howFarAway;
                _beaver.GetComponent<BeaverAI>().InvokeScared(transform.parent, dist);
            }

            GetComponent<Collider>().enabled = false;
        }
        */
    }
}
