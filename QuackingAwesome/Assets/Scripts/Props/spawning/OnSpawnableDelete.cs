using UnityEngine;

namespace Props.spawning
{
    public class OnSpawnableDelete : MonoBehaviour
    {
        public Transform positionPool;
    
        public GameObject spawnPositionPrefab;
    
        private bool _isQuitting;
    
        void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        // add position too pool of spawn-points
        private void OnDestroy()
        {
            if (_isQuitting) return;
            //Debug.Log("Stick is destroyed.");
        
            /*
            // get pool of spawn points
            GameObject pool = GameObject.Find("StickSpawnSpots");
            
            // create new spawn point
            GameObject sPoint = Instantiate(spawnPositionPrefab, transform.position, Quaternion.identity);

            // set it as child of the pool
            sPoint.transform.parent = pool.transform;
            */

            // return spawn-position back to pool of positions
            if (transform.childCount > 0)
            {
                var child = transform.GetChild(0);
                child.parent = child;
            }
        }
    }
}
