using Controllers.Duck;
using UnityEngine;

namespace Controllers.Buttons
{
    public class QuackButton : MonoBehaviour
    {
        public GameObject player;

        private QuackingBehaviour _control;

        private void Start()
        {
            _control = player.GetComponent<QuackingBehaviour>();
        }

        public void Quack()
        {
            _control.Quack();
        }
    }
}
