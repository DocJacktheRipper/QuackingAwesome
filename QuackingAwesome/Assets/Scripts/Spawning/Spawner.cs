using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawning
{
    public class Spawner : MonoBehaviour
    {
        public bool spawnOnStart;
        // how many of them should be in the world at the same time
        public int minItemsInWorld;
        public int maxItemsInWorld;

        // automatic spawns with delay etc.
        public bool updateSpawnsEnabled;
        // time between spawns
        public float spawningDelay;
        private readonly float _spawnCheckTime = 0.5f;
        private float _nextSpawnCheck;
        private int _remainingNumberOfObjectsToSpawn;
        
        public List<GameObject> objectsToSpawn;
        public GameObject targetParent;
        
        public float spawnRadius = 1.5f;

        // positions to spawn
        //public List<SpawnPoint> spawningPoints;
        public Transform spawningPointParent;

        private void Start()
        {
            if (spawnOnStart)
            {
                SpawnAtOnce(minItemsInWorld);
            }

            _remainingNumberOfObjectsToSpawn = 0;
        }

        private void Update()
        {
            if (updateSpawnsEnabled)
            {
                AutoSpawnCheck();
            }
        }
        
        #region SpawnInvokes

        private void SpawnAtOnce(int numberOfObjects)
        {
            for (var i = 0; i < numberOfObjects; i++)
            {
                Spawn();
            }
        }

        private void SpawnWithDelay(float seconds)
        {
            Invoke(nameof(Spawn), seconds);
        }
        
        protected virtual void Spawn()
        {
            if (spawningPointParent.childCount <= 0) return;
            
            var spawnPoint = GetRandomPosition();
            var positionInRadius = ApplyRadiusToRandomPosition(spawnPoint);

            InstantiateSpawnObject(positionInRadius);
            
            // for auto-update
            _remainingNumberOfObjectsToSpawn--;
        }

        #endregion
        #region RespawnInvokes
        // Only enable respawn, when not done automatically
        
        public void Respawn()
        {
            // only respawn immediately, when not automatically done
            if (updateSpawnsEnabled)
                return;

            Spawn();
        }
        
        public void RespawnWithDelay(float seconds)
        {
            if (updateSpawnsEnabled)
                return;
            SpawnWithDelay(seconds);
        }

        public void RespawnAtOnce(int numberOfObjects)
        {
            if (updateSpawnsEnabled)
                return;
            SpawnAtOnce(numberOfObjects);
        }

        #endregion
        #region HelperMethodsToSpawn

        protected Vector3 ApplyRadiusToRandomPosition(Transform spawnPoint)
        {
            var positionInRadius = (Vector3) Random.insideUnitSphere * spawnRadius;
            positionInRadius += spawnPoint.position;
            positionInRadius.y = 0f;
            return positionInRadius;
        }
        
        protected GameObject InstantiateSpawnObject(Vector3 position)
        {
            var spawned = Instantiate(GetRandomSkin(), position, Quaternion.identity); 
            spawned.transform.parent = targetParent.transform;
            RotateObjectRandomly(spawned.transform);

            return spawned;
        }

        private GameObject GetRandomSkin()
        {
            return objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
        }

        private static void RotateObjectRandomly(Transform obj)
        {
            obj.Rotate( 0f, Random.Range(0, 360f), 0f, Space.Self);
        }

        private void SetObjectsAsChildren(Transform spawnPoint, GameObject o)
        {
            o.transform.parent = targetParent.transform;    // move newly created object (stick, peas)
            spawnPoint.parent = o.transform;                // move spawn point to be child of new object
        }

        protected Transform GetRandomPosition()
        {
            var ranPos = (int) Random.Range(0, spawningPointParent.childCount);
            return spawningPointParent.GetChild(ranPos);
        } 

        #endregion

        #region AutoSpawn

        private void AutoSpawnCheck()
        {
            // only check every [time] seconds
            if (_nextSpawnCheck > Time.time)
                return;
            _nextSpawnCheck = Time.time + _spawnCheckTime;
            
            // check, if more are spawn-able
            var currentObjectsInWorld = targetParent.transform.childCount;
            if (currentObjectsInWorld >= maxItemsInWorld) return;

            // calculate how many can still be spawned
            var numberOfObjectsToSpawn = maxItemsInWorld - (currentObjectsInWorld + _remainingNumberOfObjectsToSpawn);
            _remainingNumberOfObjectsToSpawn += numberOfObjectsToSpawn;
            
            AutoSpawnLoopWithDelay(numberOfObjectsToSpawn);
        }

        private void AutoSpawnLoopWithDelay(int numberOfNewObjects)
        {
            float waitingTime = spawningDelay;
            for (int i = 0; i < numberOfNewObjects; i++)
            {
                Invoke(nameof(Spawn), waitingTime);
                waitingTime += spawningDelay;
            }
        }

        #endregion
    }
}
