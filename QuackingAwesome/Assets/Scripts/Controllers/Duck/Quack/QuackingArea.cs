using System;
using AI.Beaver;
using UnityEngine;

namespace Controllers.Duck.Quack
{
    public class QuackingArea : MonoBehaviour
    {
        private GameObject _beaver;
        private GameObject _alligator;

        // How heavy is the quack? "damage"
        public float volume;

        private void Start()
        {
            _beaver = GameObject.FindWithTag("Beaver");
            _alligator = GameObject.FindWithTag("Alligator");
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Beaver"))
            {
                QuackToBeaver();
            }
        }

        private void QuackToBeaver()
        {
            if (_beaver.GetComponent<ScaredAway>().AttemptScare(volume))
            {
                Debug.Log("is scared away");
                _beaver.GetComponent<BeaverAI>().InvokeScared(transform.parent);
            }

            GetComponent<Collider>().enabled = false;
        }
    }
}
