using UnityEngine;

namespace AI.Alligator.States
{
    public class AlligatorAI : MonoBehaviour
    {
        #region StateMachine
        
        private HandlerAI stateMachine;

        public IdleState idle;
        public ChasingState chasing;

        #endregion
        #region InitialProperties

        public float waitSecondsForSwitchingStates;
        public int chanceForSwitchingStates;

        #endregion
        
        void Start()
        {
            stateMachine = new HandlerAI();

            idle = new IdleState(gameObject, waitSecondsForSwitchingStates, chanceForSwitchingStates);
            chasing = new ChasingState();
            
            stateMachine.Initialize(idle);
        }

        void Update()
        {
            stateMachine.Execute();
        }
    }
}
