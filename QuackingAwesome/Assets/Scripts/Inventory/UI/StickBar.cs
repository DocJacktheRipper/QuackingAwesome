using Controllers.Buttons.NestMenu;
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

        public bool stayVisible;

        private Text _text;
        private int _numberSticksInNest;
        private string _numberSticksNeeded;

        void Start()
        {
            _text               = GetComponent<Text>();
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
            _text.text          = _numberSticksInNest + " / " + _numberSticksNeeded;
        }

        public void DisplayStickStatistics(bool visible)
        {
            stickStatistic.SetActive(visible);
        }
    }
}