using AI.Frog.StateMachine_Frog;
using Props.spawning;
using UnityEngine;

namespace Spawning.Animals
{
    public class FrogSpawner : Spawner
    {
        protected override void Spawn()
        {
            if (spawningPointParent.childCount <= 0) return;
            
            var spawnPoint = GetRandomPosition();
            var positionInRadius = ApplyRadiusToRandomPosition(spawnPoint);

            var frogObject = InstantiateSpawnObject(positionInRadius);
            
            ApplyNecessaryReferences(frogObject);
        }

        private void ApplyNecessaryReferences(GameObject frogObject)
        {
            var behaviourMethods = frogObject.GetComponentInChildren<BehaviourMethodsFrog>();
            behaviourMethods.peaSpawner = GetComponent<PeaSpawner>(); // give reference to pea-spawner
        }
    }
}
