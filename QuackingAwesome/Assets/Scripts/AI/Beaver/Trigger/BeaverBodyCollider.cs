using AI.Beaver.StateMachine_Beaver;
using Controllers;
using UnityEngine;

namespace AI.Beaver.Trigger
{
    public class BeaverBodyCollider : MonoBehaviour
    {
        public StateHandlerBeaver stateHandler;
        
        public void OnCollisionEnter(Collision other)
        {
            if (PlayerIsTrigger(other.collider))
            {
                return;
            }
            stateHandler.MouthTriggerEntered(other.collider);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
            stateHandler.MouthTriggerEntered(other);
        }

        private bool PlayerIsTrigger(Collider duckCollider)
        {
            if (!duckCollider.CompareTag("Player"))
            {
                return false;
            }

            //DuckDied();
            DeathOfDuck();

            return true;
        }
        
        private void DeathOfDuck()
        {
            // TODO: actually death or warning first?
            DeathBehaviour deathEvent = GameObject.Find("DeathBehaviour").GetComponent<DeathBehaviour>();
            StartCoroutine(deathEvent.DuckDied(20, this.GetComponentInParent<Collider>()));
        }
    }
}
