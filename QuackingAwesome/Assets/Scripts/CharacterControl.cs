using LeoLuz.PlugAndPlayJoystick;
using System;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody duck;

    //private float PushForce = 1f; //duck's moving force
    private float Rotation = 3.8f; //value how fast duck rotates
    public float DuckSpeed;

    private Vector3 lookDir;
    private Vector3 movement;

    private int dashFrame = 0;

    public AudioSource quack_placeholder;

    void Start()
    {
        duck = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DuckSpeed = duck.velocity.magnitude;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //determine vectors for moving and looking
        movement = new Vector3(x, 0f, z) * 2f * Time.deltaTime;
        lookDir = new Vector3(movement.x, 0f, movement.z);


        // determine method of rotation
        if (x != 0 || z != 0)
        {
            // create a smooth direction to look at using Slerp()
            Vector3 smoothDir = Vector3.Slerp(transform.forward, lookDir, Rotation * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(smoothDir);
        }

        //get input for dash and quack(attack)
        if (Input.GetButtonDown("Submit"))
        {
            dash();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            quack_placeholder.Play();
        }
    }
    void FixedUpdate()
    {
        duck.AddForce(movement, ForceMode.VelocityChange);
    }

    void dash()
    {
        //times the dash within 1 frame from exection
        if(Time.frameCount != dashFrame)
        {
            if (DuckSpeed < 0.3f)
            {
                duck.AddForce(transform.forward * 100f, ForceMode.Impulse);
            }
            else if (DuckSpeed > 0.31f && DuckSpeed < 2f) //antispam by determining max speed when dash is usable
            {
                duck.drag = 25f;
                duck.AddForce(transform.forward * 100f, ForceMode.Impulse);
                duck.drag = 1f;
            }
        }
    }
}