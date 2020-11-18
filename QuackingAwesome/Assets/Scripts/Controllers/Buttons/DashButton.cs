using Controllers.Duck;
using Controllers.Duck.Dash;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Buttons
{
    public class DashButton : MonoBehaviour
    {
        public GameObject player;
        private DashingBehaviour _control;
        
        private bool _interactable = true;
        private Button _button;
        private GameObject _cooldownOverlay;

        public KeyCode _Key;
        
        void Start()
        {
            _control = player.GetComponent<DashingBehaviour>();
            _button = GetComponent<Button>();
            _cooldownOverlay = transform.Find("cooldown_overlay").gameObject;
        }
        
        void Update()
        {
            _interactable = (_control.nextDash < Time.time);
            _button.enabled = _interactable;

            if (_interactable)
            {
                _cooldownOverlay.SetActive(false);
            }
            else
            {
                _cooldownOverlay.SetActive(true);
            }
            
            if (Input.GetKeyDown(_Key) && _button.enabled)
            {
                FadeToColor(_button.colors.pressedColor);
                _cooldownOverlay.SetActive(true);
            }
            else if (Input.GetKeyUp(_Key) && _button.enabled)
            {
                _button.onClick.Invoke();
                FadeToColor(_button.colors.normalColor);
                _cooldownOverlay.SetActive(true);
            }
            /*
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
            */
        }
        
        private void FadeToColor(Color color)
        {
            var graphic = GetComponent<Graphic>();
            graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
        }

        public void Dash()
        {
            // if cooldown is cool
            if(_interactable)
                _control.Dash();
        }
    }
}
