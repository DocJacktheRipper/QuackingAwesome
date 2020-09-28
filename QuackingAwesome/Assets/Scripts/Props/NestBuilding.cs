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
            Inventory player = other.GetComponent<Inventory>();
        
            // does player-inventory exist?
            if (player == null)
            {
                return;
            }


            // check for sticks in duck's inventory and needed for upgrade
            if (player.numberOfSticks > 0)
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

        private void TransferSticks(Inventory player)
        {
            // only use as much sticks as needed for the nest
            int diff = neededSticks - numberOfSticks;
            if ((diff - player.numberOfSticks) < 0)
            {
                numberOfSticks = neededSticks;
                player.numberOfSticks -= diff;
                RespawnSticksInWorld(diff);    // so there are the same amount of sticks in the world
                // Adjust sticks in Duckbill
                player.DeleteSticks(diff);
            }
            else
            {
                numberOfSticks += player.numberOfSticks;
                RespawnSticksInWorld(player.numberOfSticks); // so there are the same amount of sticks in the world
                player.DeleteAllSticks();
                player.numberOfSticks = 0;
            }
        }

        private void RespawnSticksInWorld(int numberOfTransferedSticks)
        {
            GameObject spawner = GameObject.Find("SpawningBehaviour");
            if (spawner == null)
                return;
            StickSpawner sp = spawner.GetComponent<StickSpawner>();
            
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
            Debug.Log(percentageOfBeingFinished);
            _nbContainer.GetChild(0).transform.localPosition 
                = new Vector3(0f, percentageOfBeingFinished * heightForDynBuilding, 0f);
        
            // percentageOfBeingFinished * heightForDynBuilding
            
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
            GameObject nestOfSticks = Instantiate(finishedNest, new Vector3(0, 0, 0), Quaternion.identity);
            // get "NestBuildingContainer" and set object as child of it
            nestOfSticks.transform.parent = _nbContainer;
            nestOfSticks.transform.position = _nbContainer.position;
        }

        private void PrintText()
        {
            var text = "Sticks in Nest: " + numberOfSticks + "/" + neededSticks;
            Debug.Log(text);
        }
    }
}
