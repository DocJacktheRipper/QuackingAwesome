using System;
using Controllers;
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
            
            //DuckDied();
            DeathOfDuck();

            return true;
        }

        private void DeathOfDuck()
        {
            DeathBehaviour deathEvent = GameObject.Find("DeathBehaviour").GetComponent<DeathBehaviour>();
            StartCoroutine(deathEvent.DuckDied(50, this.GetComponentInParent<Collider>()));
        }
    }
}
