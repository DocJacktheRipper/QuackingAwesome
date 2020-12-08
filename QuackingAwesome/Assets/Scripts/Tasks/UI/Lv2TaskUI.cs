using UI.Main_Menu.PauseMenu;
using UnityEngine;

namespace Tasks.UI
{
    public class Lv2TaskUI : TaskUI
    {
        public PauseMenuHandler pauseMenu;
        public GameObject levelComplete;
        private bool _completed;
        
        protected override void Update()
        {
            base.Update();

            if (!_completed && tasksUpdater.tasksAreCompleted)
            {
                pauseMenu.gameObject.SetActive(true);
                levelComplete.SetActive(true);
                transform.parent.gameObject.SetActive(false);
                _completed = true;
            }
        }
    }
}
