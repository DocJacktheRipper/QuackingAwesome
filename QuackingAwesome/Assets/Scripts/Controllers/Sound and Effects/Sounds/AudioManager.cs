using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers.Sound_and_Effects.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        protected AudioSource Audio;
        
        public List<AudioClip> soundClips;

        private void Start()
        {
            Audio = GetComponent<AudioSource>();
        }

        public void PlaySound()
        {
            Audio.Play();
        }

        public void PlaySound(AudioClip clip)
        {
            ApplyAudioClip(clip);
            PlaySound();
        }

        public void PlaySoundAfterDelay(AudioClip clip, float delay)
        {
            ApplyAudioClip(clip);
            Audio.PlayDelayed(delay);
        }

        public void PlayRandomSound()
        {
            ApplyRandomAudioClip();
            PlaySound();
        }

        public void ApplyAudioClip(AudioClip clip)
        {
            Audio.clip = clip;
        }
        
        public void ApplyRandomAudioClip()
        {
            var clip = GetAudioClip();
            if (clip != null)
            {
                Audio.clip = clip;
            }
        }
        
        private AudioClip GetAudioClip()
        {
            if (soundClips.Count <= 0)
                return null;
            var index = Random.Range(0, soundClips.Count);
            return soundClips[index];
        }
    }
}
