﻿using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class PopUps : MonoBehaviour
{

    //add more GameObjects if more popups are needed
    public GameObject controls; //for disabling movement controls
    public GameObject pop1; //first popup screen
    public GameObject pop2; //second
    public GameObject pop3; //third

    private void Awake()
    {
        popUp1();
    }

    private void popUp1()
    {
        pop1.SetActive(true);
        controls.SetActive(false);
        Time.timeScale = 0f;
    }

    public void popUp2()
    {
        pop2.SetActive(true);
        controls.SetActive(false);
        Time.timeScale = 0f;
    }

    public void popUp3()
    {
        pop3.SetActive(true);
        controls.SetActive(false);
        Time.timeScale = 0f;
    }

    public void buttonClick()
    {
        pop1.SetActive(false);
        pop2.SetActive(false);
        pop3.SetActive(false);
        controls.SetActive(true);
        Time.timeScale = 1f;
    }
}
