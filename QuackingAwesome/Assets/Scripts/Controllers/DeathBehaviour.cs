using System;
using Inventory;
using Nest;

using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

namespace Controllers
{
    public class DeathBehaviour : MonoBehaviour
    {
        private GameObject _duck;
        public GameObject controls;
        public GameObject youDied;
        
        public Transform alligatorMoveSpots;
        public Transform duckStartingSpot;

        // public int numberOfSticksLostInNest;
        private StickInventory _stickInventory;
        private EnergyInventory _energyInventory;
        private NestBuilding _nestBuilding;
        private int _maxSticks;

        void Start()
        {
            _duck = GameObject.FindWithTag("Player");
            _stickInventory  = _duck.GetComponent<StickInventory>();
            _energyInventory = _duck.GetComponent<EnergyInventory>();
            _nestBuilding    = GameObject.FindWithTag("Nest").GetComponent<NestBuilding>();
            _maxSticks       = _nestBuilding.neededSticks;
        }

        public IEnumerator DuckDied(float percentEnergyLost, Collider enemy, int numberOfSticksLost = -1)
        {
            Time.timeScale = 0;
            controls.SetActive(false);

            youDied.SetActive(true);
            yield return new WaitForSecondsRealtime(3);
            youDied.SetActive(false);
            
            // Drop the carried sticks
            _stickInventory.DropStick();
            
            // todo: invoke animation
            
            // Reduce the player energy
            _energyInventory.energy -= _energyInventory.energy * percentEnergyLost / 100;
            
            // Destroy the nest
            if (!_nestBuilding.NestIsFinished)
            {
                if (numberOfSticksLost < 0) numberOfSticksLost = _maxSticks;
                _nestBuilding.RemoveSticks(numberOfSticksLost);
            }
            
            // Reset duck position
            _duck.transform.SetPositionAndRotation(duckStartingSpot.position, duckStartingSpot.rotation);
            //_duck.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Debug.Log(enemy.tag);
            // Randomly respawn the killing enemy (only alligator for the moment)
            if (enemy.CompareTag("Alligator"))
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
