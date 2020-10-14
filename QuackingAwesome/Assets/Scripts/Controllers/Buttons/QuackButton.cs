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

        private void Start()
        {
            _control = player.GetComponent<QuackingBehaviour>();
            _button = GetComponent<Button>();
        }

        private void Update()
        {
            transform.GetChild(0).GetComponent<Image>().fillAmount = _control.overHeat/100;
            
            if (_control.overHeat > _control.maxOverHeat)
            {
                _button.enabled = false;
            }
            else
            {
                _button.enabled = true;
            }
        }

        public void Quack()
        {
            _control.Quack();
        }
    }
}
