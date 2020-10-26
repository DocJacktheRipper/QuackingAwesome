using UnityEngine;
using UnityEngine.UI;

namespace Nest.NestMenu
{
    public class InspectableNest : MonoBehaviour
    {
        /*
        protected Vector3 posLastFrame;
        public Camera camera;
        
        public RectTransform objectToHold;
        public Image fillImage;
        public float holdDuration = 1f;
        */
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetMouseButton(0))
            {
                var delta = Input.mousePosition - posLastFrame;
                posLastFrame = Input.mousePosition;

                var axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
                transform.rotation = Quaternion.AngleAxis(delta.magnitude * 0.1f, axis) * transform.rotation;
            }
            
            if (Input.touchCount > 0)
            {
                Touch touchInfo = Input.GetTouch(0);
                if(touchInfo.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint (objectToHold ,touchInfo.position) )
                {
                    print("is pressing and holding");
                    float remainingDuration = holdDuration -= Time.deltaTime;
                    fillImage.fillAmount = remainingDuration / holdDuration;

                    if (holdDuration <= 0)
                    {
                        //Do Stuff when timer is finished
                    }

                }
            }
            */
        }
    }
}
