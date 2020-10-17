using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DashingBehaviour : MonoBehaviour
{
    private Rigidbody _duck;
    
    //private int _dashFrame = 0;

    public float cooldown = 1;

    // time until player will be able to dash again
    public float NextDash = 0;
    
    // animation
    private Animator _animator;
    private static readonly int DoDash = Animator.StringToHash("DoDash");


    void Start()
    {
        _duck = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public void Dash()
    {
        // trigger animation
        _animator.SetTrigger(DoDash);
        
        //if (Time.time > NextDash)
        {
            _duck.AddForce(transform.forward * 100f, ForceMode.Impulse);
            
            NextDash = Time.time + cooldown;
        }
        
        /*
        //times the dash within 1 frame from exection
        if(Time.frameCount != _dashFrame)
        {
            
             * if (DuckSpeed < 0.3f)
            {
                duck.AddForce(transform.forward * 100f, ForceMode.Impulse);
            }
            else if (DuckSpeed > 0.31f && DuckSpeed < 2f) //antispam by determining max speed when dash is usable
            {
                duck.drag = 25f;
                duck.AddForce(transform.forward * 100f, ForceMode.Impulse);
                duck.drag = 1f;
            }
            
            
            _duck.AddForce(transform.forward * 100f, ForceMode.Impulse);
        }
        */
    }
}
