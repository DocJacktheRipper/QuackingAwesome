using LeavingScene.Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inventory
{
    public class EnergyInventory : MonoBehaviour
    {
        public float energy;
        
        private int _sceneID;
        private PlayerInventoryData _savedInventoryData;

        private void Start()
        {
            _sceneID = SceneManager.GetActiveScene().buildIndex;

            _savedInventoryData = GlobalControl.Instance.savedGame
                .savedScenes[_sceneID]
                .savedInventoryData;
            
            // load the player energy
            energy = _savedInventoryData.energy;
        }

        public void IncreaseEnergy(int value)
        {
            energy += value;
        }

        // saving the player energy
        private void OnDestroy()
        {
            _savedInventoryData.energy = energy;
        }
    }
}
