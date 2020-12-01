using Controllers.Sound_and_Effects.Duck;
using UnityEngine;

namespace Controllers.Sound_and_Effects
{
    public class EffectAccessorDuck : MonoBehaviour
    {
        public ParticleSystem dashEffect;
        public ParticleSystem quackEffect;
        
        // sounds
        public StickBreakingAudioManager stickAudioManager;
        public EatingAudioManager eatingAudioManager;
    }
}
