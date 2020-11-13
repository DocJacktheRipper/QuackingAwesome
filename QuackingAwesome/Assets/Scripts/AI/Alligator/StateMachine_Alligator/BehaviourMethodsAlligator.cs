using AI.StateMachine;
using UnityEngine;

namespace AI.Alligator.StateMachine_Alligator
{
    public class BehaviourMethodsAlligator : BehaviourMethods
    {
        private static readonly int DoBite = Animator.StringToHash("DoBite");
        
        #region UsefulProperties
        public float biteSpeedBonus;
        #endregion
        
        #region Bite

        public void InvokeBiting()
        {
            navigation.speed += biteSpeedBonus;
            animator.SetTrigger(DoBite);
        }

        public void StopBiting()
        {
            navigation.speed -= biteSpeedBonus;
            animator.ResetTrigger(DoBite);
        }
        
        #endregion
    }
}
