using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Buttons
{
    public class DashButton : MonoBehaviour
    {
        public GameObject player;
        private DashingBehaviour _control;
        
        private bool _interactable = true;

        void Start()
        {
            _control = player.GetComponent<DashingBehaviour>();
        }
        
        void Update()
        {
            // to look, if it was changed afterwards
            // to avoid overriding too much
            var wasInteractable = _interactable;
            
            // enable nestMenu, when cooldown is over;
            // disable when still on cooldown
            _interactable = (_control.NextDash < Time.time);

            if (wasInteractable != _interactable)
            {
                //Debug.Log("Enabled changed (dash nestMenu");
                if(_interactable)
                    GetComponent<Image>().color = Color.white;
                else
                    GetComponent<Image>().color = Color.gray;
            }
        }

        public void Dash()
        {
            // if cooldown is cool
            if(_interactable)
                _control.Dash();
        }
    }
}
