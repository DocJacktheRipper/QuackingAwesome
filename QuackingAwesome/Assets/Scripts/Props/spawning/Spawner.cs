using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Props.spawning
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
        
        /*
        private void Start()
        {
            StartCoroutine(CheckForMissingSticks());
        }

        private void CheckForMissingSticks()
        {
            if (targetParent.transform.childCount < minItemsInWorld)
            {
                
            }
        }

*/
        /*
        internal void SpawnObject()
        {
            var index = (int) Random.Range(0, spawningPoints.Count);

            SpawnPoint point = spawningPoints[index];
            GameObject obj = Instantiate(objectToSpawn, point.transform.position, Quaternion.identity);
            //spawningPoints[index].blockedPosition = true;
            spawningPoints.Remove(point);    
            Destroy(point);
            // TODO: fix, that it actually destroys obj

            obj.transform.parent = targetParent.transform;
        }
        */

        public void Spawn()
        {
            if (spawningPointParent.childCount > 0)
            {
                var spawnPoint = GetRandomPosition();
                // create object
                GameObject gameObject = Instantiate(GetRandomSkin(), spawnPoint.position, Quaternion.identity);
                SetObjectsAsChildren(spawnPoint, gameObject);
                RotateObjectRandomly(gameObject.transform);

                var spawnable = gameObject.GetComponent<OnSpawnableDelete>();
                if (spawnable != null)
                {
                    spawnable.positionPool = spawningPointParent;
                }
            }
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
/*
        IEnumerator SpawnObjectWithDelay()
        {
            SpawnObject();
            yield return WaitForSeconds(spawningDelay);
        }

        internal void SpawnObjects(int n)
        {
            StartCoroutine(SpawnObjectWithDelay());
            Invoke("SpawnObjects", spawningDelay);
        }
    */
    }
}
