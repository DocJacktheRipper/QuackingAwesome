using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inventory
{
    public class EnergyInventory : MonoBehaviour
    {
        public float energy;
        
        private int _sceneID;

        private void Start()
        {
            _sceneID = SceneManager.GetActiveScene().buildIndex;
            
            // load the player energy
            energy = GlobalControl.Instance.savedGame.
                savedScenes[_sceneID].
                savedInventoryData.
                energy;
        }

        public void IncreaseEnergy(int value)
        {
            energy += value;
        }

        // saving the player energy
        private void OnDestroy()
        {
            GlobalControl.Instance.savedGame.
                savedScenes[_sceneID].
                savedInventoryData.
                energy = energy;
        }
    }
}
