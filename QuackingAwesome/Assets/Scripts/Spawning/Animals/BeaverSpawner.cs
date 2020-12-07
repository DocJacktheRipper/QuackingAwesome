using AI.Beaver.StateMachine_Beaver;
using Nest.NPC_Nests;
using Props.spawning;
using UnityEngine;

namespace Spawning.Animals
{
    public class BeaverSpawner : Spawner
    {
        protected override void Spawn()
        {
            if (spawningPointParent.childCount <= 0) return;
            
            var spawnPoint = GetRandomPosition();
            var positionInRadius = ApplyRadiusToRandomPosition(spawnPoint);

            var beaverObject = InstantiateSpawnObject(positionInRadius);
            
            ApplyNecessaryReferences(beaverObject, spawnPoint);
        }

        private void ApplyNecessaryReferences(GameObject beaverObject, Transform beaverNest)
        {
            var behaviourMethods = beaverObject.GetComponentInChildren<BehaviourMethodsBeaver>();
            behaviourMethods.stickSpawner = GetComponent<StickSpawner>(); // give reference to stick-spawner

            // Beaver Nest (Home)
            var stateHandler = behaviourMethods.GetComponent<StateHandlerBeaver>();
            stateHandler.goingHome.beaverNest = beaverNest.GetComponent<BeaverNest>();
        }
    }
}
