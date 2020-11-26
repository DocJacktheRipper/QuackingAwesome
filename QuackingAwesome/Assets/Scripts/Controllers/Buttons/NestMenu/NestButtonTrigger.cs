using Inventory.UI;
using UnityEngine;

namespace Controllers.Buttons.NestMenu
{
    public class NestButtonTrigger : MonoBehaviour
    {
        public NestButton nestMenu;
        // public StickBar stickBar;

        private void Start()
        {
            nestMenu.ActivateNestButton();
            // stickBar.DisplayStickStatistics(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerIsTriggerEnter(other);
        }

        // Activate nest menu, when duck is swimming inside
        private void PlayerIsTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                nestMenu.ActivateNestButton();
                // stickBar.DisplayStickStatistics(true);
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
                nestMenu.DeactivateNestButton();
                // if (!stickBar.stayVisible) stickBar.DisplayStickStatistics(false);
            }
        }
    }
}
