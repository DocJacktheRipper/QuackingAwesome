using Inventory;
using UnityEngine;

namespace Controllers.Buttons.NestMenu
{
    public class HatchingEggButton : MonoBehaviour
    {
        public EnergyInventory energy;
        public float requiredEnergy;
    
        public void HatchAnEgg()
        {
            if (HasEnoughEnergyToHatchEgg())
            {
                Debug.Log("Egg can be hatched");
                // TODO implement logic
            }
            else
            {
                Debug.Log("Not enough energy");
            }
        }

        public bool HasEnoughEnergyToHatchEgg()
        {
            return (energy.energy >= requiredEnergy);
        }
    }
}
