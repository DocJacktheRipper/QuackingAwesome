using UnityEngine;

namespace Controllers.Sound_and_Effects.Sounds
{
    public class MusicManager : AudioManager
    {
        public float waitForSeconds;
        private int _sourceIndex;

        private void Start()
        {
            Audio = GetComponent<AudioSource>();
            _sourceIndex = 0;
            
            PlaySoundAfterDelay(soundClips[_sourceIndex], Random.Range(0, waitForSeconds));
        }

        private void Update()
        {
            if (!Audio.isPlaying)
            {
                IncrementIndex();
                PlaySoundAfterDelay(soundClips[_sourceIndex], waitForSeconds);
            }
        }

        private void IncrementIndex()
        {
            _sourceIndex++;
            if (_sourceIndex >= soundClips.Count)
                _sourceIndex = 0;
        }
    }
}
