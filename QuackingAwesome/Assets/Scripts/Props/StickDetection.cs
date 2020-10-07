using UnityEngine;

public class StickDetection : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (stickTrigger(other))
        {
            return;
        }
    }

    private bool stickTrigger(Collider other)
    {
        PopUps popups = GameObject.FindGameObjectWithTag("PopUpCanvas").GetComponent<PopUps>();
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ducc:D");
            popups.popUp3();
            return true;
        }
        else
        {
            return false;
        }
    }
}
