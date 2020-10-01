using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BeaverAI : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    public NavMeshAgent beaver;
    public GameObject duck;
    Collider duckCollider, beaverCollider;

    // Start is called before the first frame update
    void Start()
    {
        duckCollider = duck.GetComponent<Collider>();
        beaverCollider = beaver.GetComponent<Collider>();
        beaver = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!beaver.pathPending && beaver.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    private void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        beaver.destination = points[destPoint].position;
        destPoint = (destPoint + Random.Range(0, 18)) % points.Length;
    }

    //Sets direction of the beaver to nearest stick
    public void FetchStick(Vector3 stickPosition)
    {
        beaver.destination = stickPosition;
        Debug.Log("Fetching a stick");
    }

    //Prevents collision with beaver and duck, making the outer and larger "CapsuleCollider" to an areatrigger
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(beaverCollider, duckCollider, true);
        }
    }
}
