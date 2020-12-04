using UnityEngine;
using UnityEngine.UI;
using Nest;

namespace Inventory.UI
{
    public class StickBar : MonoBehaviour
    {
        public GameObject stickStatistic;
        public NestBuilding nest;
        public GameObject nestButton;
        public Text text;

        public bool stayVisible;

        private int _numberSticksInNest;
        private string _numberSticksNeeded;

        void Start()
        {
            Init();
        }

        void Update()
        {
            if (nest.NestIsFinished)
            {
                stickStatistic.SetActive(false);
                nestButton.SetActive(true);
            }

            else if (_numberSticksInNest != nest.numberOfSticks)
            {
                _numberSticksInNest = nest.numberOfSticks;
                SetText();
            }
        }
        
        public void Init()
        {
            _numberSticksInNest = nest.numberOfSticks;
            _numberSticksNeeded = nest.neededSticks.ToString();
            SetText();   
        }

        private void SetText()
        {
            text.text          = _numberSticksInNest + " / " + _numberSticksNeeded;
        }

        public void DisplayStickStatistics(bool visible)
        {
            stickStatistic.SetActive(visible);
        }
    }
}