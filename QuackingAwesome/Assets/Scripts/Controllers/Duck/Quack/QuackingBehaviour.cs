using System.Collections.Generic;
using Controllers.Sound_and_Effects;
using Controllers.Sound_and_Effects.Duck;
using Inventory;
using UnityEngine;

namespace Controllers.Duck.Quack
{
    public class QuackingBehaviour : MonoBehaviour
    {
        // aka cooldown
        public float overHeat;
        // public float maxOverHeat; // if above, quacking is disabled
        public float amountOfHeatPerQuack;
        public float recoverySpeed;

        public bool isRecovering;

        private GameObject _quackingCone;
        private Collider _coneCollider;

        private StickInventory _stickInventory;
        
        // animation
        private Animator _animator;
        private static readonly int DoQuack = Animator.StringToHash("DoQuack");
        // effects
        public EffectAccessorDuck effects;
        private ParticleSystem _quackEffect;
        // sound
        public QuackAudioManager audioManager;


        private void Start()
        {
            _quackingCone = GameObject.Find("QuackingCone");
            _coneCollider = _quackingCone.GetComponent<Collider>();
            _stickInventory = GetComponent<StickInventory>();

            _animator = GetComponent<Animator>();
            _quackEffect = effects.quackEffect;
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
            audioManager.PlayRandomSound();
            // animation
            _animator.SetTrigger(DoQuack);
            // effect
            _quackEffect.Play(true);
            

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
                _stickInventory.DropStick();
            }
        }
    }
}
