using AI.StateMachine;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class BehaviourMethodsBeaver : BehaviourMethods
    {
        public Transform stickContainer;


        public void AddStick(GameObject stick)
        {
            stick.transform.SetParent(stickContainer, false);
        }
        
        public void AddSpeed(float scaredAwayRunAwaySpeedBonus)
        {
            navigation.speed += scaredAwayRunAwaySpeedBonus;
        }

        public float GetSpeed()
        {
            return navigation.speed;
        }
    }
}
