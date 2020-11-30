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

        public List<GameObject> objectsToSpawn;
        public GameObject targetParent;

        // time between spawns
        public float spawningDelay;
        public float spawnRadius = 1.5f;

        // positions to spawn
        //public List<SpawnPoint> spawningPoints;
        public Transform spawningPointParent;

        //private int numberOfObjectsToSpawn = 0;

        private void Start()
        {
            if (spawnOnStart)
            {
                SpawnAtOnce(minItemsInWorld);
            }
        }

        public void SpawnAtOnce(int numberOfObjects)
        {
            for (var i = 0; i < numberOfObjects; i++)
            {
                Spawn();
            }
        }

        public void SpawnWithDelay(float seconds)
        {
            Invoke(nameof(Spawn), seconds);
        }
        
       
        public void Spawn()
        {
            if (spawningPointParent.childCount <= 0) return;
            
            var spawnPoint = GetRandomPosition();

            
            var positionInRadius = (Vector3) Random.insideUnitSphere * spawnRadius;
            positionInRadius += spawnPoint.position;
            positionInRadius.y = 0f; 
            var spawned = Instantiate(GetRandomSkin(), positionInRadius, Quaternion.identity); 
            spawned.transform.parent = targetParent.transform;
            RotateObjectRandomly(spawned.transform);
        }

        private GameObject GetRandomSkin()
        {
            return objectsToSpawn[Random.Range(0, objectsToSpawn.Count - 1)];
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

        private Transform GetRandomPosition()
        {
            var ranPos = (int) Random.Range(0, spawningPointParent.childCount);
            return spawningPointParent.GetChild(ranPos);
        } 
        
    }
}
