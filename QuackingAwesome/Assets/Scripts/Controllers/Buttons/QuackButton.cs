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

        public void Quack()
        {
            _control.Quack();
        }
    }
}
