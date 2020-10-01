using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuackingBehaviour : MonoBehaviour
{
    public AudioSource quack_placeholder;
    
    
    public void Quack()
    {
        quack_placeholder.Play();

        if (transform.childCount > 0)
        {
            DropSticks();
        }
    }

    private void DropSticks()
    {
        
    }
}
