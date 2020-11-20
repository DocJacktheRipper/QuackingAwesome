using AI.StateMachine;
using Inventory;
using UnityEngine;

namespace AI.Beaver.StateMachine_Beaver
{
    public class BehaviourMethodsBeaver : BehaviourMethods
    {
        public StickInventory stickInventory;

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
            stickInventory.AddStick(other.transform);
        }
    }
}
