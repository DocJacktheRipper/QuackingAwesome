using LeoLuz.PlugAndPlayJoystick;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    
    private Rigidbody duck;
    public float PushForce = 13f;
    public float Rotation = 3.8f;
    public float DuckSpeed;
    
    private Vector3 lookDir;
    private Vector3 movement;

    public bool EnableDebugLog = true;
   
    void Start()
    {
        duck = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DuckSpeed = duck.velocity.magnitude;
        // get input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool buttonA = Input.GetButtonDown("Fire1");
        bool buttonB = Input.GetButtonDown("Fire2");

        // create our movement vector for player movement, and use it to set our look vector for rotation
        movement = new Vector3(h, 0f, v) * PushForce * Time.deltaTime;
        duck.AddForce(movement, ForceMode.VelocityChange);
        lookDir = new Vector3(movement.x, 0f, movement.z);

        // determine method of rotation
        if (h != 0 || v != 0)
        {
            // create a smooth direction to look at using Slerp()
            Vector3 smoothDir = Vector3.Slerp(transform.forward, lookDir, Rotation * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(smoothDir);
        }

        //function for dash and attack(quack)
        if(buttonA == true && DuckSpeed < 7)
        {
            if(DuckSpeed < 1)
            {
                duck.AddForce(transform.forward * 150f, ForceMode.Impulse);
                if(EnableDebugLog)
                    Debug.Log(duck.drag + "PRESSED DRAG");
            }
            if(EnableDebugLog)
                Debug.Log("buttonA");
            duck.drag = 50;
            duck.AddForce(transform.forward * 300f, ForceMode.Impulse);
            if(EnableDebugLog)
                Debug.Log(duck.drag + "PRESSED DRAG");
        } 
        else
        {
            duck.drag = 1;
            if(EnableDebugLog)
                Debug.Log(duck.drag + "UNPRESSED DRAG");  
        }
        if(buttonB == true)
        {
            if(EnableDebugLog)
                Debug.Log("buttonB");
        }

    }

    void FixedUpdate()
    {
       
    }
}
