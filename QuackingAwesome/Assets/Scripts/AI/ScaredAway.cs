using UnityEngine;

namespace AI
{
    public class ScaredAway : MonoBehaviour
    {
        // Who invoked it?
        public Transform source;
        
        // when duck quacks to beaver
        public bool isScared;
        public float currentChance;
        public float minChance;
        public float chanceDropRate;
        public float howFarAway;
        public float runAwaySpeedBonus;

        void Update()
        {
            if (currentChance > minChance)
            {
                currentChance -= chanceDropRate;
            }

            isScared = false;
        }

        float IncreaseChance(float amount)
        {
            return currentChance += amount;
        }

        /// <summary>
        /// Each Quack has chance to scare away.
        /// </summary>
        /// <param name="heaviness">amount of increasing chance</param>
        /// <returns>Whether attempt was successful</returns>
        public bool AttemptScare(float heaviness)
        {
            float range = Random.Range(0, 100);

            Debug.Log(range+"/"+(currentChance+heaviness));
            
            if (range < IncreaseChance(heaviness))
            {
                isScared = true;
                currentChance = minChance;
            }
        
            return isScared;
        }
    }
}
