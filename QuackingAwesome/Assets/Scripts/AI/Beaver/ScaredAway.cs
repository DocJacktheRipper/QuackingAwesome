﻿using UnityEngine;

namespace AI.Beaver
{
    public class ScaredAway : MonoBehaviour
    {
        // when duck quacks to beaver
        public bool isScared;
        public float currentChance;
        public float minChance;
        public float chanceDropRate;

        private BeaverAI _movingBeaver;
        void Start()
        {
            _movingBeaver = GetComponent<BeaverAI>();
        }

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

            Debug.Log(range+"/"+currentChance);
            
            if (range < IncreaseChance(heaviness))
            {
                isScared = true;
                currentChance = minChance;
            }
        
            return isScared;
        }
    }
}
