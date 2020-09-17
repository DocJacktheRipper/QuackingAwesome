using LeoLuz.PlugAndPlayJoystick;
using System;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody duck;
    public float PushForce = 15f; //duck's moving force
    public float Rotation = 3.5f; //value how fast duck rotates
    public float DuckSpeed;

    private Vector3 lookDir;
    private Vector3 movement;

    private int dashFrame = 0;

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
        movement = new Vector3(x, 0f, z) * PushForce * Time.deltaTime;
        lookDir = new Vector3(movement.x, 0f, movement.z);


        // determine method of rotation
        if (x != 0 || z != 0)
        {
            // create a smooth direction to look at using Slerp()
            Vector3 smoothDir = Vector3.Slerp(transform.forward, lookDir, Rotation * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(smoothDir);
        }

        //get input for dash and quack(attack)
        if (Input.GetButtonDown("Fire1"))
        {
            dash();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            quack();
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
            if (DuckSpeed < 1f)
            {
                duck.AddForce(transform.forward * 250f, ForceMode.Impulse);
            }
            else if (DuckSpeed > 1.01f && DuckSpeed < 8f) //antispam by determining max speed when dash is usable
            {
                duck.drag = 50f;
                duck.AddForce(transform.forward * 400f, ForceMode.Impulse);
                duck.drag = 1f;
            }
        }
    }

    void quack()
    {

    }
}
