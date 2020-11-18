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
                visualSteps[countEnabledSteps].isOn = true;
                countEnabledSteps++;
            }
        }

        public bool MoreUpgradesPossible()
        {
            return (countEnabledSteps < visualSteps.Count);
        }
    }
}
