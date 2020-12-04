using UnityEngine; //using UnityEditor.Experimental.GraphView;

namespace Controllers.Duck
{
    public class CharacterControl : MonoBehaviour
    {
        //DONT CHANGE THE SPEED OR REMOVE GRAVITY FROM UNITY
        //DUCK DOESNT FLY AWAY WHEN USING GRAVITY AND A BOX COLLIDER FOR WATER
        private Rigidbody _duck;

        public float rotation = 3.8f; //value how fast duck rotates

        private readonly float _baseSpeed = 2f;
        private float _speed;

        private Vector3 _lookDir;
        private Vector3 _movement;
        private float x;
        private float z;

        private Animator _animator;
        private int IsSwimming = Animator.StringToHash("IsSwimming");


        void Start()
        {
            _duck = GetComponent<Rigidbody>();
            _speed = _baseSpeed;
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            //determine vector for looking
            _lookDir = new Vector3(_movement.x, 0f, _movement.z);

            // determine method of rotation
            if (x != 0 || z != 0)
            {
                // create a smooth direction to look at using Slerp()
                Vector3 smoothDir = Vector3.Slerp(transform.forward, _lookDir, rotation * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(smoothDir);
                _animator.SetBool(IsSwimming, true);
            }
            else
            {
                _animator.SetBool(IsSwimming, false);
            }
        }

        void FixedUpdate()
        {
            _movement = new Vector3(x, 0f, z) * (_speed * Time.deltaTime);
            _duck.AddForce(_movement, ForceMode.VelocityChange);
        }

        public void AddSpeedModifier(float multiplier)
        {
            _speed = _baseSpeed * multiplier;
            
            Debug.Log("New speed: "+_speed);
        }
    }
}