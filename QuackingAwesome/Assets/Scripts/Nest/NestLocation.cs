﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestLocation : MonoBehaviour
{
    public float distance;
    public GameObject duck;
    public GameObject arrow;
    public Transform nestPos;

    void Update()
    {
        distance = Vector3.Distance(duck.transform.position, nestPos.transform.position);
        if(distance > 3f)
        {
            nestPosition();
        } 
        else
        {
            // arrow.SetActive(false);
            arrow.GetComponent<Renderer>().enabled = false;
        }
    }

    private void nestPosition()
    {
        //arrow.SetActive(true);
        arrow.GetComponent<Renderer>().enabled = true;
        arrow.transform.rotation = Quaternion.LookRotation(nestPos.position - transform.position);
    }
}
