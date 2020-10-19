using System;
using Controllers.Duck;
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
        private Image filling;

        public KeyCode _Key;

        private void Start()
        {
            _control = player.GetComponent<QuackingBehaviour>();
            _button = GetComponent<Button>();
            filling = transform.GetChild(0).GetComponent<Image>();
        }

        private void Update()
        {
            filling.fillAmount = _control.overHeat/100;
            
            if(_control.isRecovering)
            {
                filling.color = new Color(1,0,0, 0.88f);
                _button.enabled = false;
            }
            else
            {
                filling.color = new Color(1, 0, 0, 0.75f);
                _button.enabled = true;
            }

            if (Input.GetKeyDown(_Key) && _button.enabled)
            {
                FadeToColor(_button.colors.pressedColor);
            }
            else if (Input.GetKeyUp(_Key) && _button.enabled)
            {
                _button.onClick.Invoke();
                FadeToColor(_button.colors.normalColor);
            }
            /*
            if (_control.overHeat > _control.maxOverHeat)
            {
                _button.enabled = false;
            }
            else
            {
                _button.enabled = true;
            }*/
        }

        private void FadeToColor(Color color)
        {
            var graphic = GetComponent<Graphic>();
            graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
        }
        
        public void Quack()
        {
            _control.Quack();
        }
    }
}
