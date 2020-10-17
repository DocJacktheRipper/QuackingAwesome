using System;
using UnityEngine;

namespace Controllers.Duck.Quack
{
    public class QuackingBehaviour : MonoBehaviour
    {
        public AudioSource quackPlaceholder;

        // aka cooldown
        public float overHeat;
        public float maxOverHeat; // if above, quacking is disabled
        public float amountOfHeatPerQuack;
        public float recoverySpeed;

        public bool isRecovering;

        private GameObject _quackingCone;
        private Collider _coneCollider;
        
        // animation
        private Animator _animator;
        private static readonly int DoQuack = Animator.StringToHash("DoQuack");

        private void Start()
        {
            _quackingCone = GameObject.Find("QuackingCone");
            _coneCollider = _quackingCone.GetComponent<Collider>();

            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            overHeat -= recoverySpeed;
            if (overHeat <= 0)
            {
                isRecovering = false;
                overHeat = 0;
            }
        }

        public void Quack()
        {
            // sound
            quackPlaceholder.Play();
            // animation
            _animator.SetTrigger(DoQuack);
            

            if (transform.childCount > 0)
            {
                DropSticks();
            }

            _coneCollider.enabled = true;

            overHeat += amountOfHeatPerQuack;
            if(overHeat >= 100)
            {
                isRecovering = true;
            }
        }

        private void DropSticks()
        {
            var stickHolder = transform.Find("CarriedSticks");

            for (int i = 0; i < stickHolder.childCount; i++)
            {
                // let it fall down
                var stick = stickHolder.GetChild(i);
                
                // TODO: how to fix this?
            }
        }
    }
}
