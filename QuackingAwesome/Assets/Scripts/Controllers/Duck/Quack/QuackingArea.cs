using System;
using AI;
using AI.Alligator.StateMachine_Alligator;
using AI.Beaver.StateMachine_Beaver;
using AI.Beaver.StateMachine_Beaver.ConcreteStates;
using AI.Beaver.Trigger;
using AI.Frog.StateMachine_Frog;
using JetBrains.Annotations;
using UnityEngine;

namespace Controllers.Duck.Quack
{
    public class QuackingArea : MonoBehaviour
    {
        // How heavy is the quack? "damage"
        private float _baseVolume = 10;
        private float _volumeMultiplier;
        public float volume;
        private Collider _quackArea;

        private void Start()
        {
            _quackArea = GetComponent<Collider>();
        }

        private void OnTriggerStay(Collider other)
        {
            QuackToAnimal(other.gameObject);
        }

        private void QuackToAnimal(GameObject animal)
        {
            var ai = animal.transform.parent.Find("AI");
            if (ai == null)
            {
                //Debug.Log("AI not found!");
                return;
            }
            ScaredAway scared = ai.Find("Scare").GetComponent<ScaredAway>();

            if (scared.AttemptScare(volume))
            {
                if (animal.CompareTag("Beaver"))
                {
                    ScareBeaver(scared, ai);
                }

                if (animal.CompareTag("Frog"))
                {
                    ScareFrog(scared, ai);
                }

                if (animal.CompareTag("Alligator"))
                {
                    ScareAlligator(scared, ai);
                }
            }
            
            _quackArea.enabled = false;
        }

        private void ScareBeaver(ScaredAway scared, [NotNull] Transform ai)
        {
            if (ai == null) throw new ArgumentNullException(nameof(ai));
            Debug.Log("Beaver is scared away");
            var stateHandler = ai.GetComponent<StateHandlerBeaver>();
                
            // prepare and change states
            scared.source = transform;
            stateHandler.ChangeState(stateHandler.scared);
        }

        private void ScareFrog(ScaredAway scared, [NotNull] Transform ai)
        {
            if (ai == null) throw new ArgumentNullException(nameof(ai));
            Debug.Log("Frog is scared away");
            var stateHandler = ai.GetComponent<StateHandlerFrog>();
                
            // prepare and change states
            scared.source = transform;
            stateHandler.ChangeState(stateHandler.scared);
        }

        private void ScareAlligator(ScaredAway scared, Transform ai)
        {
            Debug.Log("Alligator is scared away");
            var stateHandler = ai.GetComponent<StateHandlerAlligator>();
                
            // prepare and change states
            scared.source = transform;
            stateHandler.ChangeState(stateHandler.scared);
        }
        
        /*
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
        */

        public void SetVolumeMultiplier(float multiplier)
        {
            _volumeMultiplier = multiplier;
            volume = _baseVolume * _volumeMultiplier;
        }
    }
}
