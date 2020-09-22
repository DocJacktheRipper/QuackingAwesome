using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AlligatorScript : MonoBehaviour
{
    private Transform target;
    public Transform[] moveSpots;

    public float speed;
    public float speedboost;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get to next spot
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }

    private void OnTriggerStay(Collider other)
    {
        Inventory playerDuck = other.GetComponent<Inventory>();

        // check, if duck is in range
        if (playerDuck == null)
        {
            return;
        }
        
        AimForDuck(playerDuck);
    }

    private void AimForDuck(Inventory playerDuck)
    {
        target = playerDuck.transform;
        speed += speedboost;
    }

    private void OnTriggerExit(Collider other)
    {
        Inventory playerDuck = other.GetComponent<Inventory>();

        // check, if duck is still in range
        if (playerDuck == null)
        {
            speed -= speedboost;
            GetNewTarget();
            
            return;
        }
    }

    // select randomly the new target
    private void GetNewTarget()
    {
        // TODO
    }
}
