using System;
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
    }
}
