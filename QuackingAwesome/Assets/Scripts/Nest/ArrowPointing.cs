using System;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;

namespace Nest
{
    public class ArrowPointing : MonoBehaviour
    {
        public Renderer arrowMesh;
        public Transform duck;
        private NestBuilding _currentNest;
        private float _distance;
        public List<NestBuilding> nests;
        public float distanceForActivating = 3f;
        public StickBar stickBar;

        private void Update()
        {
            if (nests.Count <= 0)
                return;

            var previousClosest = _currentNest;
            SetClosestNest();
            if (previousClosest != _currentNest)
            {
                SetStickBar();
            }
            
            if (_distance < distanceForActivating)
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
            var closest = nests[0];
            float dist =  Vector3.Distance(closest.transform.position, duck.position);

            for (int i = 1; i < nests.Count; i++)
            {
                var tempDist =  Vector3.Distance(nests[i].transform.position, duck.position);
                if (tempDist < dist)
                {
                    dist = tempDist;
                    closest = nests[i];
                }
            }

            _currentNest = closest;
            _distance = dist;
        }

        private void SetStickBar()
        {
            stickBar.nest = _currentNest;
            stickBar.Init();
        }

        private void DeactivateArrow()
        {
            arrowMesh.enabled = false;
        }

        private void ActivateAndSetArrow()
        {
            arrowMesh.enabled = true;
            arrowMesh.transform.rotation = Quaternion.LookRotation(_currentNest.transform.position - duck.position);
        }
    }
}
