using UnityEngine;

namespace Nest
{
    public class NestEffectTrigger : MonoBehaviour
    {
        public ParticleSystem nestIsFinishedEffect;

        public void NestFinishedEffect()
        {
            nestIsFinishedEffect.Play();
        }
    }
}
