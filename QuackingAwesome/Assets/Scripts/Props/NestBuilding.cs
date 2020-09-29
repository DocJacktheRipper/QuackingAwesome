using Inventory;
using Props.spawning;
using UnityEngine;

namespace Props
{
    public class NestBuilding : MonoBehaviour
    {
        public int numberOfSticks;
        public int neededSticks;

        public bool enableDynamicBuilding;
        public float heightForDynBuilding;
    
        public GameObject finishedNest;

        private Transform _nbContainer;

        private void Start()
        {
            _nbContainer = transform.Find("NestBuildingContainer");
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
            
                // visually showing progress (?)
                if (enableDynamicBuilding)
                {
                    BuildNestDynamically();
                    return;
                }
            
                PrintText();
            
                if (numberOfSticks >= neededSticks)
                {
                    BuildNest();
                }
            }
        }

        private void TransferSticks(StickInventory player)
        {
            // only use as much sticks as needed for the nest
            var diff = neededSticks - numberOfSticks;
            if ((diff - player.GetNumberOfSticks()) < 0)
            {
                Debug.Log("More sticks in inventory than needed");
                numberOfSticks = neededSticks;
                // so there are the same amount of sticks in the world
                RespawnSticksInWorld(diff);    
                // Adjust sticks in Duckbill
                player.RemoveSticks(diff);
            }
            else
            {
                numberOfSticks += player.GetNumberOfSticks();
                // so there are the same amount of sticks in the world
                RespawnSticksInWorld(player.GetNumberOfSticks()); 
                player.RemoveSticks(player.GetNumberOfSticks());
                player.DeleteAllVisualSticks();
            }
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
            if (_nbContainer.childCount <= 0)
            {    
                BuildNest();
            }
        
            // set y pos based on heightForDynBuilding and number of sticks in nest
            var percentageOfBeingFinished = 1 - (neededSticks - numberOfSticks) * 1.0f / neededSticks;
            //Debug.Log(percentageOfBeingFinished);
            _nbContainer.GetChild(0).transform.localPosition 
                = new Vector3(0f, percentageOfBeingFinished * heightForDynBuilding, 0f);
        }

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

        private void PrintText()
        {
            var text = "Sticks in Nest: " + numberOfSticks + "/" + neededSticks;
            Debug.Log(text);
        }
    }
}
