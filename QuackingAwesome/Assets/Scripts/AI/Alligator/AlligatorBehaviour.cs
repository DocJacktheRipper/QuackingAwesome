using UnityEngine;

namespace AI.Alligator
{
    public class AlligatorBehaviour : MonoBehaviour
    {
        public Transform target;
        private bool _isFocussingDuck;
        private int _targetIndex;
        public Transform[] moveSpots;

        private Transform _parentObject;
        public float speed;
        public float speedBoost;
    
        // Start is called before the first frame update
        void Start()
        {
            _parentObject = transform.parent;
            _isFocussingDuck = false;
            target = moveSpots[0];
            GetNewTarget();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // get to next spot
            var targetPosition = target.position;

            _parentObject.position = Vector3.MoveTowards(_parentObject.position, targetPosition, speed * Time.deltaTime);
        
            /*
            // rotate aligator to face direction
            var lookDir = position.normalized - transform1.position.normalized ; ///new Vector3(movement.x, 0f, movement.z);
            transform.rotation = Quaternion.LookRotation(lookDir);
            */
        
            if (_isFocussingDuck)
            {
                return;
            }
            // distance still big, so continue swimming towards it
            if ((Vector3.Distance(_parentObject.position, targetPosition) > 0.2f))
            {
                return;
            }
            
            _targetIndex++;
            if (_targetIndex >= moveSpots.Length)
            {
                _targetIndex = 0;
            }

            target = moveSpots[_targetIndex];
        }

        private void OnTriggerEnter(Collider other)
        {
            var playerDuck = other.tag;

            // check, if duck is in range
            if (playerDuck != "Player")
            {
                return;
            }
        
            AimForDuck(other.gameObject);
        }

        private void AimForDuck(GameObject playerDuck)
        {
            _isFocussingDuck = true;
            target = playerDuck.transform;
            speed += speedBoost;
        }

        private void OnTriggerExit(Collider other)
        {
            var triggerTag = other.tag;
        
            var isPlayer = triggerTag.Equals("Player");
        
            // Debug.Log("fucus: "+_isFocussingDuck + "\nIs player: "+isPlayer);
        
            // check, if duck has exited detection range
            if (_isFocussingDuck && isPlayer)
            {
                Debug.Log("Can't find duck anymore. Go back to my place.");
                speed -= speedBoost;
                GetNewTarget();
            }
        }
    

        // select randomly the new target
        private void GetNewTarget()
        {
            _targetIndex = Random.Range(0, moveSpots.Length);
            target = moveSpots[_targetIndex];
        }
    }
}
