using AI.StateMachine;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class BehaviourMethodsBeaver : BehaviourMethods
    {
       public void AddSpeed(float scaredAwayRunAwaySpeedBonus)
        {
            navigation.speed += scaredAwayRunAwaySpeedBonus;
        }

        public float GetSpeed()
        {
            return navigation.speed;
        }

        public void CollectStick(Collider other)
        {
            throw new System.NotImplementedException();
        }
    }
}
