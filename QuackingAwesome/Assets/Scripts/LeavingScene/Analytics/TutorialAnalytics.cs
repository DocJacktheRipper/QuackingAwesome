using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

namespace Analytics
{
    public class TutorialAnalytics : MonoBehaviour
    {
        public enum LevelPlayState {InProgress, NestCompleted}
        private LevelPlayState _state = LevelPlayState.InProgress;
        private Scene _thisScene;
        
        private float _secondsElapsed = 0;
        private float _totalPeas = 0;
        private float _lostPeas = 0;
        //private float _peasEnded = 0;
        private int _totalSticks = 0;
        private int _deaths = 0;
        private int _quacks = 0;
        private int _dashs = 0;
        private int _beaver = 0;
        private int _alligator = 0;

        void Awake () {
            _thisScene = SceneManager.GetActiveScene();
            AnalyticsEvent.LevelStart(_thisScene.name, 
                _thisScene.buildIndex);
        }
        
        public void SetLevelPlayState(LevelPlayState newState){
            this._state = newState;
            switch (newState)
            {
                case LevelPlayState.NestCompleted:
                    Dictionary<string, object> customParams = new Dictionary<string, object>();
                    customParams.Add("secondsPlayed", _secondsElapsed);
                    customParams.Add("deaths", _deaths);
                    UnityEngine.Analytics.Analytics.CustomEvent("TutorialNestBuild", customParams);
                    break;
                default:
                    break;
            }
        }
        
        public void IncreaseScore(float peas){
            _totalPeas += peas;
        }
        
        public void LostPeas(float peas){
            _lostPeas += peas;
        }
        
        public void IncrementSticks(){
            _totalSticks++;
        }

        public void IncrementDeaths(string enemy){
            _deaths++;
            switch (enemy)
            {
                case "Alligator": 
                    _alligator++;
                    break;
                case "Beaver":
                    _beaver++;
                    break;
                default:
                    break;
            }
        }

        public void IncrementQuacks(){
            _quacks++;
        }
        
        public void IncrementDashes(){
            _dashs++;
        }

        void Update(){
            _secondsElapsed += Time.deltaTime;
        }

        private void OnDestroy()
        {
            Dictionary<string, object> customParams = new Dictionary<string, object>();
            customParams.Add("secondsPlayed", _secondsElapsed);
            customParams.Add("totalPeasCollected", _totalPeas);
            customParams.Add("totalPeasLost", _lostPeas);
            //customParams.Add("peasEnded", _peasEnded);
            customParams.Add("totalSticksCollected", _totalSticks);
            customParams.Add("totalQuack", _quacks);
            customParams.Add("totalDash", _dashs);
            //customParams.Add("deaths", _deaths);
            customParams.Add("beaverKill", _beaver);
            customParams.Add("alligatorKill", _alligator);
            
            switch(this._state){
                case LevelPlayState.NestCompleted:
                    AnalyticsEvent.LevelComplete(_thisScene.name,
                        _thisScene.buildIndex, 
                        customParams);
                    break;
                case LevelPlayState.InProgress:
                default:
                    AnalyticsEvent.LevelQuit(_thisScene.name,
                        _thisScene.buildIndex, 
                        customParams);
                    break;
            }
        }
    }
}

