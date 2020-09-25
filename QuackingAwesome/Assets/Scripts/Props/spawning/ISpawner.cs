using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Props.spawning
{
    public class Spawner : MonoBehaviour
    {
        public bool spawnOnStart;
        // how many of them should be in the world at the same time
        public int minItemsInWorld;
        public int maxItemsInWorld;

        public GameObject objectToSpawn;
        public GameObject targetParent;

        // time between spawns
        public float spawningDelay;

        // positions to spawn
        public List<SpawnPoint> spawningPoints;

        //private int numberOfObjectsToSpawn = 0;

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
        internal void SpawnObject()
        {
            var index = (int) Random.Range(0, spawningPoints.Count);

            SpawnPoint point = spawningPoints[index];
            GameObject obj = Instantiate(objectToSpawn, point.transform.position, Quaternion.identity);
            //spawningPoints[index].blockedPosition = true;
            spawningPoints.Remove(point);    
            Destroy(point);
            // TODO: fix, that it actually destroys obj
        
            var transformEulerAngles = obj.transform.eulerAngles;
            transformEulerAngles.x = Random.Range(0, 360);

            obj.transform.parent = targetParent.transform;
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
