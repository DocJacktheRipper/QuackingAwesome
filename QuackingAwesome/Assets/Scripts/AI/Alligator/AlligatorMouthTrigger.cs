using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AI.Alligator
{
    public class AlligatorMouthTrigger : MonoBehaviour
    {
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
            // load current scene
            var scene = SceneManager.GetActiveScene(); 
            // load a scene without tutorial popups
            if(scene.name == "Starting")
            {
                SceneManager.LoadScene("StartingNoPopUps");
            } 
            else
            {
                SceneManager.LoadScene(scene.name);
            }
            
        }
    }
}
