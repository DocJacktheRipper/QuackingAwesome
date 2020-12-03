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
        private Image _filling;
        
        private Color _recoverColor;
        private Color _normalColor;

        public KeyCode key;
        private Graphic _graphic;

        void Start()
        {
            _graphic = GetComponent<Graphic>();
            _control = player.GetComponent<DashingBehaviour>();
            _button = GetComponent<Button>();
            _filling = transform.GetChild(0).GetComponent<Image>();
        }
        
        void Update()
        {
            _interactable = (_control.nextDash < Time.time);
            _button.enabled = _interactable;

            _filling.fillAmount = (_control.nextDash - Time.time) / _control.cooldown;
            
            if (Input.GetKeyDown(key) && _button.enabled)
            {
                FadeToColor(_button.colors.pressedColor);
            }
            else if (Input.GetKeyUp(key) && _button.enabled)
            {
                _button.onClick.Invoke();
                FadeToColor(_button.colors.normalColor);
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
            _graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
        }

        public void Dash()
        {
            // if cooldown is cool
            if(_interactable)
                _control.Dash();
        }
    }
}
