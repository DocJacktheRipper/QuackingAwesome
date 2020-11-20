using UnityEngine;
using System.Collections;

using Inventory;
using Nest;

namespace Controllers
{
    public class DeathBehaviour : MonoBehaviour
    {
        private GameObject _duck;
        public GameObject controls;
        public GameObject youDied;
        public GameObject respawnToNest;
        
        public Transform alligatorMoveSpots;
        public Transform duckStartingSpot;

        // public int numberOfSticksLostInNest;
        private StickInventory _stickInventory;
        private EnergyInventory _energyInventory;
        private NestBuilding _nestBuilding;
        private int _maxSticks;
        private DucklingsInventory _ducklings;

        private Collider _enemy;

        void Start()
        {
            _duck = GameObject.FindWithTag("Player");
            
            _stickInventory  = _duck.GetComponent<StickInventory>();
            _energyInventory = _duck.GetComponent<EnergyInventory>();
            _ducklings       = _duck.GetComponent<DucklingsInventory>();
            
            _nestBuilding    = GameObject.FindWithTag("Nest").GetComponent<NestBuilding>();
            _maxSticks       = _nestBuilding.neededSticks;

        }

        public IEnumerator DuckDied(float percentEnergyLost, Collider enemy, int numberOfSticksLost = -1)
        {
            Time.timeScale = 0;
            controls.SetActive(false);

            _enemy = enemy;

            // Drop the carried sticks
            _stickInventory.DropStick();
            
            // todo: invoke animation
            
            // Reduce the player energy
            _energyInventory.energy -= _energyInventory.energy * percentEnergyLost / 100;
            
            // Destroy the nest
            if (_nestBuilding.NestIsFinished)
            {
                respawnToNest.SetActive(true);
            }
            else
            {
                youDied.SetActive(true);
                yield return new WaitForSecondsRealtime(3);
                youDied.SetActive(false);
                
                if (numberOfSticksLost < 0) numberOfSticksLost = _maxSticks;
                _nestBuilding.RemoveSticks(numberOfSticksLost);
                
                BackToNest(0);
            }

        }

        public void BackToNest(int lostHatchlings)
        {
            _ducklings.RemoveDucklings(lostHatchlings);
            
            // Reset duck position
            _duck.transform.SetPositionAndRotation(duckStartingSpot.position, duckStartingSpot.rotation);
            _duck.GetComponent<Rigidbody>().velocity = Vector3.zero;
            
            respawnToNest.SetActive(false);
            
            // Randomly respawn the killing enemy (only alligator for the moment)
            if (_enemy.CompareTag("Alligator"))
            {
                GameObject.Find("Alligator")
                    .transform
                    .SetPositionAndRotation(
                        alligatorMoveSpots.GetChild(
                            UnityEngine.Random.Range(0, alligatorMoveSpots.childCount)).position,
                        Quaternion.identity
                    );
            }

            Time.timeScale = 1;
            controls.SetActive(true);
        }

    }
}
