using AI.Frog.StateMachine_Frog;
using Controllers.Sound_and_Effects;
using Inventory;
using UnityEngine;

namespace AI.Frog.Trigger
{
    public class FrogBodyCollider : MonoBehaviour
    {
        public StateHandlerFrog stateHandler;
        public int energyValue;
        private static readonly int DoPickAndKeep = Animator.StringToHash("DoPickAndKeep");

        private void OnTriggerEnter(Collider other)
        {
            if (PlayerIsTrigger(other))
            {
                return;
            }
            stateHandler.MouthTriggerEntered(other);
        }

        private bool PlayerIsTrigger(Collider other)
        {
            var inventory = other.GetComponent<EnergyInventory>();
            if (inventory == null)
                return false;

            inventory.IncreaseEnergy(energyValue);

            TriggerDuckAnimationAndSound(other.gameObject);
            
            DeleteFrog();
            return true;
        }

        private void DeleteFrog()
        {
            var frog = transform.parent.parent.gameObject;
            Destroy(frog);
        }

        private void TriggerDuckAnimationAndSound(GameObject duck)
        {
            var animator = duck.GetComponent<Animator>();
            animator.SetTrigger(DoPickAndKeep);

            var effectAccessor = duck.transform.Find("Effects").GetComponent<EffectAccessorDuck>();
            var audioManager = effectAccessor.eatingAudioManager;
            audioManager.PlayRandomSound();
        }
    }
}
