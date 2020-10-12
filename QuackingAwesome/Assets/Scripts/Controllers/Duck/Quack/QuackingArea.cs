using System;
using UnityEngine;

namespace Controllers.Duck.Quack
{
    public class QuackingArea : MonoBehaviour
    {
        private GameObject _beaver;
        private GameObject _alligator;


        private void Start()
        {
            _beaver = GameObject.FindWithTag("Beaver");
            _alligator = GameObject.FindWithTag("Alligator");
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Beaver"))
            {
                QuackToBeaver();
            }
        }

        private void QuackToBeaver()
        {
        }
    }
}
