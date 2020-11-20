using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Controllers.Sound_and_Effects
{
    public class MuteToggle : MonoBehaviour
    {
        public Toggle musicToggle;
        public float musicVolume;
        public Toggle soundToggle;
        public float soundVolume;

        public AudioMixer audioMixer;

        private bool _soundIsOn = true;
        private bool _musicIsOn = true;

        private void Start()
        {
            // check, if already turned on/of to show it correctly
            if (audioMixer.GetFloat("sound_volume", out var volume))
            {
                if (volume <= -70f)
                {
                    _soundIsOn = false;
                    soundToggle.isOn = _soundIsOn;
                }
            }
            if (audioMixer.GetFloat("music_volume", out volume))
            {
                if (volume <= -70f)
                {
                    _musicIsOn = false;
                    musicToggle.isOn = _musicIsOn;
                }
            }
        }

        #region Toggle

        public void ToggleSoundOnValueChange()
        {
            ToggleSoundOnValueChange((_soundIsOn = !_soundIsOn));
        }

        public void ToggleSoundOnValueChange(bool audioIn)
        {
            //AudioListener.volume = audioIn ? 1 : 0;
            audioMixer.SetFloat("sound_volume", audioIn ? soundVolume : -80f);
        }

        public void ToggleMusicOnValueChange()
        {
            ToggleMusicOnValueChange((_musicIsOn = !_musicIsOn));
        }

        public void ToggleMusicOnValueChange(bool audioIn)
        {
            //AudioListener.volume = audioIn ? 1 : 0;
            audioMixer.SetFloat("music_volume", audioIn ? musicVolume : -80f);
        }
        
        #endregion
    }
}
