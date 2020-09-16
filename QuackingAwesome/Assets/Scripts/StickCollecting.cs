using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCollecting : MonoBehaviour
{
    public int count = 0;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("touched");
        if (other.gameObject.CompareTag("Player"))
        {
            count++;
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I'm triggered");
    }
}
