using UnityEngine;

namespace UI.StartMenu
{
    [RequireComponent(typeof(Animation))]
    public class CloudAnimationEvent : MonoBehaviour
    {
        public MenuHandler menuHandler;
        public Animation _animation;
        /*
        private Animation _animation;

        private void Start()
        {
            _animation = GetComponent<Animation>();
        }*/

        public void InvokeAnimation()
        {
            _animation.Play();
        }

        public void IsFullyRevealed()
        {
            Debug.Log("IsFullyRevealed");
            menuHandler.CheckLevels();
        }
    }
}
