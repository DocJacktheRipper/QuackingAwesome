using Props;
using UnityEngine;

namespace Controllers.Buttons.NestMenu
{
    public class NestButton : MonoBehaviour
    {
        public NestBuilding nest;

        public GameObject nestMenuUI;
    

        public void ActivateNestButton()
        {
            if(nest.NestIsFinished)
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
