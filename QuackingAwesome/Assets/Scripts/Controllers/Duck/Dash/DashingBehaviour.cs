using Controllers.Sound_and_Effects;
using Controllers.Sound_and_Effects.Duck;
using UnityEngine;

namespace Controllers.Duck.Dash
{
    public class DashingBehaviour : MonoBehaviour
    {
        private Rigidbody _duck;
    
        //private int _dashFrame = 0;

        public float cooldown = 1;

        // time until player will be able to dash again
        public float nextDash = 0;
    
        // animation
        private Animator _animator;
        private static readonly int DoDash = Animator.StringToHash("DoDash");
        // particle effects
        public EffectAccessorDuck effects;
        private ParticleSystem _dashEffect;
        // sound
        public DashAudioManager audioManager;


        void Start()
        {
            _duck = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _dashEffect = effects.dashEffect;
        }

        public void Dash()
        {
            // sound
            audioManager.PlayRandomSound();
            // trigger animation
            _animator.SetTrigger(DoDash);
            // trigger effect
            if (_dashEffect != null)
            {
                _dashEffect.Play();
            }
        
            //if (Time.time > NextDash)
            {
                _duck.AddForce(transform.forward * 100f, ForceMode.Impulse);
            
                nextDash = Time.time + cooldown;
            }
        }
    }
}
