using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationQuack : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            Quack();
        }
        else
        {
            anim.SetBool("IsQuacking", false);
        }
    }

    public void Quack()
    {
        anim.SetBool("IsQuacking", true);
    }
}
