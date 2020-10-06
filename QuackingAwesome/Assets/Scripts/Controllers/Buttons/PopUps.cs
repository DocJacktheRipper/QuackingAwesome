using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{

    //add more GameObjects if more popups are needed
    public GameObject popUps;
    public GameObject pop1; //first popup screen
    public GameObject pop2; //second
    public GameObject pop3; //third

    public Collider duck;
    public GameObject pea;
    public Collider[] sticks;

    private void Awake()
    {
        popUp1();
    }

    private void Update()
    {
     
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    Debug.Log("entered popup trigger");
    //    if(other.gameObject.tag == "pea")
    //    {
    //        popUp2();
    //    }
    //    if(other.gameObject.tag == "stick" ||
    //       other.gameObject.tag == "stick 2" ||
    //       other.gameObject.tag == "stick 3")
    //    {
    //        popUp3();
    //    }
    //}

    private void popUp1()
    {
        pop1.SetActive(true);
        Time.timeScale = 0f;
    }

    private void popUp2()
    {
        pop2.SetActive(true);
        Time.timeScale = 0f;
    }

    private void popUp3()
    {
        pop3.SetActive(true);
        Time.timeScale = 0f;
    }

    public void buttonClick()
    {
        pop1.SetActive(false);
        pop2.SetActive(false);
        pop3.SetActive(false);
        Time.timeScale = 1f;
    }

}
