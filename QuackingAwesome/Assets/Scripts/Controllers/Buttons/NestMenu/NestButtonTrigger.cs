using UnityEngine;

namespace Controllers.Buttons.NestMenu
{
    public class NestButtonTrigger : MonoBehaviour
    {
        public NestButton button;

        private void Start()
        {
            button.ActivateNestButton();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerIsTriggerEnter(other);
        }

        // Activate nest menu, when duck is swimming outside
        private void PlayerIsTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                button.ActivateNestButton();
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            PlayerIsTriggerExit(other);
        }

        // Deactivate nest menu, when duck is swimming outside
        private void PlayerIsTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                button.DeactivateNestButton();
            }
        }
    }
}
