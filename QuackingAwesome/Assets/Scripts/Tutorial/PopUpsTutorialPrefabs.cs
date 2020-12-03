using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class PopUpsTutorialPrefabs : MonoBehaviour
{

    //add more GameObjects if more popups are needed
    public GameObject controls; //for disabling movement controls
    public GameObject pop1; //first popup screen
    public GameObject pop2; //second
    public GameObject pop3; //third

    //checking if popup is displayed more than once
    public bool p2;
    public bool p3;

    private void Awake()
    {
        p2 = true;
        p3 = true;
        StartCoroutine(DisplayTutorialAdvice(pop1));
    }
    
    private IEnumerator DisplayTutorialAdvice(GameObject popUp)
    {
        popUp.SetActive(true);
        yield return new WaitForSecondsRealtime(6);
        popUp.SetActive(false);
    }
    

    public void popUp2()
    {
        p2 = false;
        StartCoroutine(DisplayTutorialAdvice(pop2));
    }

    public void popUp3()
    {
        p3 = false;
        StartCoroutine(DisplayTutorialAdvice(pop3));
    }

}
