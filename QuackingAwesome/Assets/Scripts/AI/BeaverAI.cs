using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BeaverAI : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent beaver;

    public GameObject sticks;

    private float Distance;


    // Start is called before the first frame update
    void Start()
    {
        sticks = GameObject.Find("CollectableSicks");
        beaver = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(beaver.transform.position, sticks.transform.position);
        //Debug.Log(Distance);

        if (!beaver.pathPending && beaver.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        if(points.Length == 0)
        {
            return;
        }

        beaver.destination = points[destPoint].position;
        destPoint = (destPoint + Random.Range(0, 18)) % points.Length;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Beaver")
        {
            Debug.Log("collide");
        }
    }

    private void FetchStick()
    {
        
    }

   
}
