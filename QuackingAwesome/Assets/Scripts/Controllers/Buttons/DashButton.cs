﻿using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Buttons
{
    public class DashButton : MonoBehaviour
    {
        public GameObject player;

        private DashingBehaviour _control;

        public Text displayCooldown;

        private bool _interactable = true;

        void Start()
        {
            _control = player.GetComponent<DashingBehaviour>();
        }
        
        void Update()
        {
            // to look, if it was changed afterwards
            var wasInteractable = _interactable;
            
            // enable button, when cooldown is over;
            // disable when still on cooldown
            _interactable = (_control.NextDash < Time.time);

            if (wasInteractable != _interactable)
            {
                Debug.Log("Enabled changed (dash button");
                if(_interactable)
                    GetComponent<Image>().color = Color.white;
                else
                    GetComponent<Image>().color = Color.gray;
            }
        }

        public void Dash()
        {
            if(_interactable)
                _control.Dash();
        }
    }
}
