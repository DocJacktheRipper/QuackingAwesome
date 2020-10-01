using UnityEngine; //using UnityEditor.Experimental.GraphView;

namespace Controllers.Duck
{
    public class CharacterControl : MonoBehaviour
    {

        //DONT CHANGE THE SPEED OR REMOVE GRAVITY FROM UNITY
        //DUCK DOESNT FLY AWAY WHEN USING GRAVITY AND A BOX COLLIDER FOR WATER
        private Rigidbody _duck;

        private float _rotation = 3.8f; //value how fast duck rotates
        public float duckSpeed; //for debugging to see the speed

        private Vector3 _lookDir;
        private Vector3 _movement;
        private float x;
        private float z;


        void Start()
        {
            _duck = GetComponent<Rigidbody>();
        }

        void Update()
        {
            duckSpeed = _duck.velocity.magnitude;
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            //determine vector for looking
            _lookDir = new Vector3(_movement.x, 0f, _movement.z);

            // determine method of rotation
            if (x != 0 || z != 0)
            {
                // create a smooth direction to look at using Slerp()
                Vector3 smoothDir = Vector3.Slerp(transform.forward, _lookDir, _rotation * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(smoothDir);
            }

            /*
            //get input for dash and quack(attack)
            if (Input.GetButtonDown("Submit"))
            {
                Dash();
            }
            if (Input.GetButtonDown("Fire3"))
            {
                quack_placeholder.Play();
            }
            */
        }
        void FixedUpdate()
        {
            _movement = new Vector3(x, 0f, z) * 2f * Time.deltaTime;
            _duck.AddForce(_movement, ForceMode.VelocityChange);
        }

    }
}