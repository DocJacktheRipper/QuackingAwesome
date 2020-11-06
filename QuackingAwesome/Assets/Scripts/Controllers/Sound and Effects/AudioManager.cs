using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers.Sound_and_Effects
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _audio;
        
        public List<AudioClip> soundClips;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void PlaySound()
        {
            _audio.Play();
        }

        public void PlaySound(AudioClip clip)
        {
            ApplyAudioClip(clip);
            PlaySound();
        }

        public void PlayRandomSound()
        {
            ApplyRandomAudioClip();
            PlaySound();
        }

        public void ApplyAudioClip(AudioClip clip)
        {
            _audio.clip = clip;
        }
        
        public void ApplyRandomAudioClip()
        {
            var clip = GetAudioClip();
            if (clip != null)
            {
                _audio.clip = clip;
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
