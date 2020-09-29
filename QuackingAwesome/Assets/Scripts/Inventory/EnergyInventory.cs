using UnityEngine;

namespace Inventory
{
    public class EnergyInventory : MonoBehaviour
    {
        public ProgressBar progressBar;

        private void Start()
        {
            progressBar.BarValue = 10;
        }

        public void IncreaseEnergy(int value)
        {
            progressBar.BarValue += value;
        }
    }
}
