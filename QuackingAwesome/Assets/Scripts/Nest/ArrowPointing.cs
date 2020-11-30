using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nest
{
    public class ArrowPointing : MonoBehaviour
    {
        public Renderer arrowMesh;
        public Transform duck;
        public Transform currentNest;
        public float distance;
        public List<Transform> nests;
        public float distanceForActivating = 3f;

        private void Update()
        {
            if (nests.Count <= 0)
                return;

            SetClosestNest();
            if (distance < distanceForActivating)
            {
                DeactivateArrow();
            }
            else
            {
                ActivateAndSetArrow();
            }
        }

        private void SetClosestNest()
        {
            Transform closest = nests[0];
            float dist =  Vector3.Distance(closest.position, duck.position);

            for (int i = 1; i < nests.Count; i++)
            {
                var tempDist =  Vector3.Distance(nests[i].position, duck.position);
                if (tempDist < dist)
                {
                    dist = tempDist;
                    closest = nests[i];
                }
            }

            currentNest = closest;
            distance = dist;
        }

        private void DeactivateArrow()
        {
            arrowMesh.enabled = false;
        }

        private void ActivateAndSetArrow()
        {
            arrowMesh.enabled = true;
            arrowMesh.transform.rotation = Quaternion.LookRotation(currentNest.position - duck.position);
        }
    }
}
