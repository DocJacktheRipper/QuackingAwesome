using System;
using AI.Beaver;
using AI.Beaver.StateMachine_Beaver;
using UnityEngine;

namespace Controllers.Duck.Quack
{
    public class QuackingArea : MonoBehaviour
    {
        private GameObject _beaver;
        private GameObject _alligator;

        // How heavy is the quack? "damage"
        public float volume;
        private Collider _quackArea;

        private void Start()
        {
            _beaver = GameObject.FindWithTag("Beaver");
            _alligator = GameObject.FindWithTag("Alligator");
            _quackArea = GetComponent<Collider>();
        }

        private void OnTriggerStay(Collider other)
        {
            GameObject beaver;
            if ( (beaver = other.gameObject).CompareTag("Beaver"))
            {
                Debug.Log("Beaver trigger");
                QuackToBeaver(beaver);
            }
        }

        private void QuackToBeaver(GameObject beaver)
        {
            var ai = beaver.transform.Find("AI");
            ScaredAway scared = ai.Find("Scare").GetComponent<ScaredAway>();

            if (scared.AttemptScare(volume))
            {
                Debug.Log("is scared away");
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
