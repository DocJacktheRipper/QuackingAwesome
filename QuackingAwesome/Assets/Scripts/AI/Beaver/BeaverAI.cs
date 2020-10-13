﻿//using Boo.Lang;

using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI.Beaver
{
    public class BeaverAI : MonoBehaviour
    {
        public Transform[] points;
        private int destPoint = 0;
        public NavMeshAgent beaverNavigation;
        public GameObject duck;
        Collider duckCollider, beaverCollider;

        // Start is called before the first frame update
        void Start()
        {
            duckCollider = duck.GetComponent<Collider>();
            beaverCollider = beaverNavigation.GetComponent<Collider>();
            beaverNavigation = GetComponent<NavMeshAgent>();
            GotoNextPoint();
        }

        // Update is called once per frame
        void Update()
        {
            if (!beaverNavigation.pathPending && beaverNavigation.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }

        private void GotoNextPoint()
        {
            if (points.Length == 0)
            {
                return;
            }
            beaverNavigation.destination = points[destPoint].position;
            destPoint = (destPoint + Random.Range(0, 18)) % points.Length;
        }

        /// <summary>
        /// Change target to position opposite of given source with fixed distance to that.
        /// </summary>
        /// <param name="source">from where to go away</param>
        /// <param name="distance">how far away is the new target point</param>
        public void InvokeScared(Transform source, float distance)
        {
            var position = transform.position;
            var dir = (position - source.position);
            var relPoint = Vector3.Normalize(dir) * distance;
            var point = position + relPoint;

            beaverNavigation.destination = point;
        }

        //Sets direction of the beaverNavigation to nearest stick
        public void FetchStick(Vector3 stickPosition)
        {
            beaverNavigation.destination = stickPosition;
            Debug.Log("Fetching a stick");
        }

        //Prevents collision with beaverNavigation and duck, making the outer and larger "CapsuleCollider" to an areatrigger
        private void OnCollisionEnter(Collision collision)
        {
            Physics.IgnoreCollision(beaverCollider, duckCollider, true);
        }
    }
}
