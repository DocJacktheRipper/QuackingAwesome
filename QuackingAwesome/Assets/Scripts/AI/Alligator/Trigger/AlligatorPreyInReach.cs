using System;
using AI.Alligator.States;
using UnityEngine;

namespace AI.Alligator
{
    public class AlligatorPreyInReach : MonoBehaviour
    {
        public BehaviourAlligator alligator;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                alligator.Bite();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                alligator.ExitBite();
            }
        }
    }
}
