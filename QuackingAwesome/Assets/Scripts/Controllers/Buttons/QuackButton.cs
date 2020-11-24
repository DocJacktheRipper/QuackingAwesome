using Controllers.Duck.Quack;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Buttons
{
    public class QuackButton : MonoBehaviour
    {
        public GameObject player;

        private QuackingBehaviour _control;
        private Button _button;
        private Image _filling;

        private Color _recoverColor;
        private Color _normalColor;

        public KeyCode key;
        private Graphic _graphic;

        private void Start()
        {
            _graphic = GetComponent<Graphic>();
            _control = player.GetComponent<QuackingBehaviour>();
            _button = GetComponent<Button>();
            _filling = transform.GetChild(0).GetComponent<Image>();
            _recoverColor = new Color(1,0,0, 0.88f);
            _normalColor = new Color(1, 0, 0, 0.75f);
        }

        private void Update()
        {
            _filling.fillAmount = _control.overHeat/100;
            
            if(_control.isRecovering)
            {
                _filling.color = _recoverColor;
                _button.enabled = false;
            }
            else
            {
                _filling.color = _normalColor;
                _button.enabled = true;
            }

            if (Input.GetKeyDown(key) && _button.enabled)
            {
                FadeToColor(_button.colors.pressedColor);
            }
            else if (Input.GetKeyUp(key) && _button.enabled)
            {
                _button.onClick.Invoke();
                FadeToColor(_button.colors.normalColor);
            }
        }

        private void FadeToColor(Color color)
        {
            _graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
        }
        
        public void Quack()
        {
            _control.Quack();
        }
    }
}
