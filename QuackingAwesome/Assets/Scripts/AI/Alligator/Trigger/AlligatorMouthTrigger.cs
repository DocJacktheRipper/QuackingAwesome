using System;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AI.Alligator
{
    public class AlligatorMouthTrigger : MonoBehaviour
    {
        public bool reloadOnDeath;
        
        private void OnTriggerEnter(Collider other)
        {
            if(PlayerIsTrigger(other))
            {
                return;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (PlayerIsTrigger(other.collider))
            {
                return;
            }
        }

        private bool PlayerIsTrigger(Collider collider)
        {
            if (!collider.CompareTag("Player"))
            {
                return false;
            }

            DuckDied();

            return true;
        }

        private void DuckDied()
        {
            if (reloadOnDeath)
            {
                // load current scene
                var scene = SceneManager.GetActiveScene().name;

                SceneManager.LoadScene(scene);
            }
            else
            {
                DeathOfDuck();
            }
        }

        private void DeathOfDuck()
        {
            DeathBehaviour deathEvent = GameObject.Find("DeathBehaviour").GetComponent<DeathBehaviour>();
            deathEvent.DuckDied();
        }
    }
}
