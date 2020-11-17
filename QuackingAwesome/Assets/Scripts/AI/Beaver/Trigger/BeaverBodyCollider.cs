using AI.Beaver.StateMachine_Beaver;
using Controllers;
using Props.Sticks;
using UnityEngine;

namespace AI.Beaver.Trigger
{
    public class BeaverBodyCollider : MonoBehaviour
    {
        public StateHandlerBeaver stateHandler;
        public Transform stickContainer;
        
        public void OnCollisionEnter(Collision other)
        {
           // stateHandler.MouthColliderEntered(other);
            if (PlayerIsTrigger(other.collider))
            {
                return;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
        }

        /*
        private bool StickIsTrigger(Collider stickCollider)
        {
            if (!stickCollider.CompareTag("Stick"))
            {
                return false;
            }

            var stick = stickCollider.GetComponent<StickTriggerEnter>();
            stick.PickStick(stickContainer);
            
            return true;
        }
        */

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
            DeathBehaviour deathEvent = GameObject.Find("DeathBehaviour").GetComponent<DeathBehaviour>();
            deathEvent.DuckDied();
        }
    }
}
