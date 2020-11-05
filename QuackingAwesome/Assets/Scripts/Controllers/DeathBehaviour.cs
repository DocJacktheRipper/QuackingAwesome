using Nest;
using UnityEngine;

namespace Controllers
{
    public class DeathBehaviour : MonoBehaviour
    {
        private GameObject _duck;

        public int numberOfSticksLostInNest;

        void Start()
        {
            _duck = GameObject.FindWithTag("Player");
        }

        public void DuckDied()
        {
            // reset duck position
            _duck.transform.SetPositionAndRotation(new Vector3(0, 0.1f, 0), Quaternion.identity);

            // todo: invoke animation, drop carried sticks, reset enemies
        
            // nest
            GameObject nest = GameObject.FindWithTag("Nest");
            var nestBuilding = nest.GetComponent<NestBuilding>();
            nestBuilding.RemoveSticks(numberOfSticksLostInNest);
        }
    }
}
