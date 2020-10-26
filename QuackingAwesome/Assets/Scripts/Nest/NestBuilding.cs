﻿using Inventory;
using Props.spawning;
using UnityEngine;

namespace Nest
{
    public class NestBuilding : MonoBehaviour
    {
        public int numberOfSticks;
        public int neededSticks;

        public bool enableDynamicBuilding;
        public float heightForDynBuilding;

        // true, when nest is built
        public bool NestIsFinished { get; private set; }

        private Transform _nbContainer;
        private NestEffectTrigger _effectTrigger;

        private void Start()
        {
            _nbContainer = transform.Find("NestBuildingContainer");
            _effectTrigger = GetComponent<NestEffectTrigger>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerIsTrigger(other);
        }

        private void PlayerIsTrigger(Collider other)
        {
            StickInventory player = other.GetComponent<StickInventory>();
        
            // does player-inventory exist?
            if (player == null)
            {
                return;
            }


            // check for sticks in duck's inventory and needed for upgrade
            if (player.GetNumberOfSticks() > 0)
            {
                //Debug.Log("Transfering sticks now");
                TransferSticks(player);
                
                if (numberOfSticks >= neededSticks)
                {
                    SetNestFinished();
                }
            
                // visually showing progress (?)
                if (enableDynamicBuilding)
                {
                    BuildNestDynamically();
                }

                /*
                PrintText();
            
                if (numberOfSticks >= neededSticks)
                {
                    BuildNest();
                }
                */
            }
        }

        private void SetNestFinished()
        {
            if (!NestIsFinished)
            {
                Debug.Log("nest is finished!");
            
                // Todo: make audio sound
            
                // make particle effect
                _effectTrigger.NestFinishedEffect();
            }
            
            NestIsFinished = true;
           
        }

        private void TransferSticks(StickInventory player)
        {
            // only use as much sticks as needed for the nest
            var diff = neededSticks - numberOfSticks;
            int numOfTransferedStick;
            if ((diff - player.GetNumberOfSticks()) < 0)
            {
                Debug.Log("More sticks in inventory than needed");
                numberOfSticks = neededSticks;
                
                numOfTransferedStick = diff;
            }
            else
            {
                numOfTransferedStick = player.GetNumberOfSticks();
                numberOfSticks += numOfTransferedStick;
                
                player.DeleteAllVisualSticks();
            }

            // so there are the same amount of sticks in the world
            RespawnSticksInWorld(numOfTransferedStick);    
            // Adjust sticks in Duckbill
            player.RemoveSticks(numOfTransferedStick);
        }

        private static void RespawnSticksInWorld(int numberOfTransferedSticks)
        {
            var spawner = GameObject.Find("SpawningBehaviour");
            if (spawner == null)
                return;
            var sp = spawner.GetComponent<StickSpawner>();
            
            sp.SpawnAtOnce(numberOfTransferedSticks);
        }

        private void BuildNestDynamically()
        {
            /*
            if (_nbContainer.childCount <= 0)
            {    
                BuildNest();
            }
            */
            _nbContainer.GetChild(0).gameObject.SetActive(true);
        
            // set y pos based on heightForDynBuilding and number of sticks in nest
            var percentageOfBeingFinished = 1 - (neededSticks - numberOfSticks) * 1.0f / neededSticks;
            //Debug.Log(percentageOfBeingFinished);
            _nbContainer.GetChild(0).transform.localPosition 
                = new Vector3(0f, percentageOfBeingFinished * heightForDynBuilding, 0f);
        }

        /*
        private void BuildNest()
        {
            // is already built a nest on rock?
            if (_nbContainer.childCount > 0)
            {
                Debug.Log("already a nest on it");
                return;
            }
        
            // create nest object
            var nestOfSticks = Instantiate(finishedNest, _nbContainer, true);
            // get "NestBuildingContainer" and set object as child of it
            nestOfSticks.transform.position = _nbContainer.position;
        }
        */
    }
}
