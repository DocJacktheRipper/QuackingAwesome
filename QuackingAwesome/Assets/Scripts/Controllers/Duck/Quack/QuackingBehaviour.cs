using UnityEngine;

namespace Controllers.Duck.Quack
{
    public class QuackingBehaviour : MonoBehaviour
    {
        public AudioSource quackPlaceholder;

        private GameObject _quackingCone;
        private Collider _coneCollider;
        
        // animation
        private Animator _animator;

        private void Start()
        {
            _quackingCone = GameObject.Find("QuackingCone");
            _coneCollider = _quackingCone.GetComponent<Collider>();

            //_animator = GetComponent<Animator>();
        }

        public void Quack()
        {
            // sound
            quackPlaceholder.Play();
            // animation
            //_animator.Play("Base Layer.Quack");

            if (transform.childCount > 0)
            {
                DropSticks();
            }

            _coneCollider.enabled = true;
        }

        private void DropSticks()
        {
            
        }
    }
}
