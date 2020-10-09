using Props;
using UnityEngine;

namespace Controllers.Buttons.NestMenu
{
    public class NestButton : MonoBehaviour
    {
        public NestBuilding nest;

        public GameObject nestButton;
        public GameObject nestMenuUI;
    

        public void ActivateNestButton()
        {
            if(nest.NestIsFinished)
                nestButton.SetActive(true);
        }

        public void DeactivateNestButton()
        {
            nestButton.SetActive(false);
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
