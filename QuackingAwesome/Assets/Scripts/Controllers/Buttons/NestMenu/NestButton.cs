using Props;
using UnityEngine;

namespace Controllers.Buttons.NestMenu
{
    public class NestButton : MonoBehaviour
    {
        public GameObject nest;
        private NestBuilding _nestInventory;

        public GameObject nestMenuUI;
    
        void Start()
        {
            _nestInventory = nest.GetComponent<NestBuilding>();
        }

        public void ActivateNestButton()
        {
            gameObject.SetActive(true);
        }

        public void DeactivateNestButton()
        {
            gameObject.SetActive(false);
        }

        public void ExpandNestMenu()
        {
            nestMenuUI.SetActive(true);
        }

        public void CloseNestMenu()
        {
            nestMenuUI.SetActive(false);
        }
    }
}
