using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Controllers.Sound_and_Effects
{
    public class MuteToggle : MonoBehaviour
    {
        public Toggle musicToggle;
        public Toggle soundToggle;

        public AudioMixer audioMixer;

        private bool soundIsOn = true;
        private bool musicIsOn = true;

        private void Start()
        {
            // check, if already turned on/of to show it correctly
            if (audioMixer.GetFloat("sound_volume", out var volume))
            {
                if (volume == 0)
                {
                    soundToggle.isOn = false;
                }
            }
            if (audioMixer.GetFloat("music_volume", out volume))
            {
                if (volume == 0)
                {
                    musicToggle.isOn = false;
                }
            }
        }

        #region Toggle

        public void ToggleSoundOnValueChange()
        {
            ToggleMusicOnValueChange((soundIsOn = !soundIsOn));
        }

        public void ToggleSoundOnValueChange(bool audioIn)
        {
            // AudioListener.volume = audioIn ? 1 : 0;
            audioMixer.SetFloat("sound_volume", audioIn ? 1 : 0);
        }

        public void ToggleMusicOnValueChange()
        {
            ToggleMusicOnValueChange((musicIsOn = !musicIsOn));
        }

        public void ToggleMusicOnValueChange(bool audioIn)
        {
            // AudioListener.volume = audioIn ? 1 : 0;
            audioMixer.SetFloat("music_volume", audioIn ? 1 : 0);
        }
        
        #endregion
    }
}
