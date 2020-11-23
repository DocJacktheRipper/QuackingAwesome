using System.Collections.Generic;
using UnityEngine;

using Controllers.Sound_and_Effects;
using Controllers.Sound_and_Effects.Duck;
using Inventory;
using Analytics;

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
        private float _activeColliderTime;

        private StickInventory _stickInventory;
        
        private TutorialAnalytics _analytics;
        
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

            _analytics = GameObject.Find("Analytics").GetComponent<TutorialAnalytics>();
        }

        private void Update()
        {
            overHeat -= recoverySpeed;
            if (overHeat <= 0)
            {
                isRecovering = false;
                overHeat = 0;
            }

            if (_activeColliderTime < Time.time)
            {
                _coneCollider.enabled = false;
            }
        }

        public void Quack()
        {
            _analytics.IncrementQuacks();
            
            // sound
            audioManager.PlayRandomSound();
            // animation
            _animator.SetTrigger(DoQuack);
            // effect
            //_quackEffect.Play(true);


            DropSticks();
            

            // enable trigger
            _coneCollider.enabled = true;
            _activeColliderTime = Time.time + 0.2f;

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
