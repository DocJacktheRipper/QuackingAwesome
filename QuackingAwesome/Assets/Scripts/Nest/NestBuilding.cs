using UnityEngine;
using Inventory;
using Analytics;
using LeavingScene.Save;
using Props.spawning;

namespace Nest
{
    public class NestBuilding : MonoBehaviour
    {
        // public int numberOfSticks;
        public int neededSticks;

        public bool enableDynamicBuilding;
        public float heightForDynBuilding;

        // true, when nest is built
        // public bool NestIsFinished { get; private set; }

        private Transform _nbContainer;
        private NestEffectTrigger _effectTrigger;
        
        private TutorialAnalytics _analytics;
        
        public NestData nestDataToSave;

        private AudioSource _audio;

        private void Start()
        {
            _nbContainer = transform.Find("NestBuildingContainer");
            _effectTrigger = GetComponent<NestEffectTrigger>();
            _analytics = GameObject.Find("Analytics").GetComponent<TutorialAnalytics>();
            _audio = GetComponent<AudioSource>();

            if (enableDynamicBuilding)
            {
                BuildNestDynamically();
            }
        }

        private void OnTriggerStay(Collider other)
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
            if (player.GetNumberOfSticks() <= 0) return;
            
            
            //Debug.Log("Transferring sticks now");
            TransferSticks(player);
                
            if (nestDataToSave.numberOfSticks >= neededSticks)
            {
                SetNestFinished();
            }
            
            // visually showing progress (?)
            if (enableDynamicBuilding)
            {
                BuildNestDynamically();
            }
            else
            {
                BuildNestOfComponents();
            }
        }

        private void SetNestFinished()
        {
            _analytics.SetLevelPlayState(TutorialAnalytics.LevelPlayState.NestCompleted);
            
            if (!nestDataToSave.nestIsFinished)
            {
                Debug.Log("nest is finished!");
            
                // Todo: make audio sound
            
                // make particle effect
                _effectTrigger.NestFinishedEffect();
            }
            
            nestDataToSave.nestIsFinished = true;
            GlobalControl.Instance.savedGame.savedMillstonesData.nestBuild++;

            _audio.Play();
        }

        private void TransferSticks(StickInventory player)
        {
            // only use as much sticks as needed for the nest
            var diff = neededSticks - nestDataToSave.numberOfSticks;
            int numOfTransferedStick;
            if ((diff - player.GetNumberOfSticks()) < 0)
            {
                //Debug.Log("More sticks in inventory than needed");
                nestDataToSave.numberOfSticks = neededSticks;
                
                numOfTransferedStick = diff;
            }
            else
            {
                numOfTransferedStick = player.GetNumberOfSticks();
                nestDataToSave.numberOfSticks += numOfTransferedStick;
                //player.DeleteAllVisualSticks();
            }
            
            player.MoveSticksToNest(numOfTransferedStick, _nbContainer);

            // so there are the same amount of sticks in the world
            RespawnSticksInWorld(numOfTransferedStick);    
            // Adjust sticks in Duckbill
            //player.RemoveSticks(numOfTransferedStick);
        }

        private static void RespawnSticksInWorld(int numberOfTransferedSticks)
        {
            var spawner = GameObject.Find("SpawningBehaviour");
            if (spawner == null)
                return;
            var sp = spawner.GetComponent<StickSpawner>();
            
            sp.RespawnAtOnce(numberOfTransferedSticks);
        }

        private void BuildNestDynamically()
        {
            _nbContainer.GetChild(0).gameObject.SetActive(true);
        
            // set y pos based on heightForDynBuilding and number of sticks in nest
            var percentageOfBeingFinished = 1 - (neededSticks - nestDataToSave.numberOfSticks) * 1.0f / neededSticks;
            _nbContainer.GetChild(0).transform.localPosition 
                = new Vector3(0f, percentageOfBeingFinished * heightForDynBuilding, 0f);
        }

        private void BuildNestOfComponents()
        {
            var percentageOfBeingFinished = 1 - (neededSticks - nestDataToSave.numberOfSticks) * 1.0f / neededSticks;

            if (percentageOfBeingFinished > 0)
            {
                _nbContainer.GetChild(0).gameObject.SetActive(true);
            }
            if (percentageOfBeingFinished > 0.6)
            {
                _nbContainer.GetChild(1).gameObject.SetActive(true);
            }
            if (percentageOfBeingFinished >= 1)
            {
                _nbContainer.GetChild(2).gameObject.SetActive(true);
            }
        }

        public void RemoveSticks(int numberOfSticksLostInNest)
        {
            nestDataToSave.numberOfSticks -= numberOfSticksLostInNest;

            if (nestDataToSave.numberOfSticks < 0)
            {
                nestDataToSave.numberOfSticks = 0;
            }

            if (nestDataToSave.numberOfSticks < neededSticks)
            {
                nestDataToSave.nestIsFinished = false;
            }
            
            BuildNestDynamically();
        }
        
        #region GET
        public int GETNumberOfSticks()
        {
            return nestDataToSave.numberOfSticks;
        }
        
        public bool GETNestFinished()
        {
            return nestDataToSave.nestIsFinished;
        }
        #endregion
    }
}
