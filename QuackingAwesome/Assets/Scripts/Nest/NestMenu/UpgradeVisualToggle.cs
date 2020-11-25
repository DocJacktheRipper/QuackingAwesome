using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nest.NestMenu
{
    public class UpgradeVisualToggle : MonoBehaviour
    {
        public List<Toggle> visualSteps;
        public int countEnabledSteps;

        private void Awake()
        {
            foreach (var step in visualSteps)
            {
                if (!step.isOn)
                    return;

                countEnabledSteps++;
            }
        }

        public void EnableNextStep()
        {
            if (MoreUpgradesPossible())
            {
                Debug.Log("Upgrade Toggle activated: " + countEnabledSteps);
                visualSteps[countEnabledSteps].isOn = true;
                countEnabledSteps++;
            }
        }

        public bool MoreUpgradesPossible()
        {
            Debug.Log(countEnabledSteps+"/"+visualSteps.Count);
            return (countEnabledSteps < visualSteps.Count);
        }
    }
}
