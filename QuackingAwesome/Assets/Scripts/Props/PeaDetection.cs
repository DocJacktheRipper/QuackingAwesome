using UnityEngine;

public class PeaDetection : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(peaTrigger(other))
        {
            return;
        }
    }

    private bool peaTrigger(Collider other)
    {
        PopUps popups = GameObject.FindGameObjectWithTag("PopUpCanvas").GetComponent<PopUps>(); 
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ducc:D");
            popups.popUp2();
            return true;
        } 
        else
        {
            Debug.Log("det är inte duck yes??");
            return false;
        }
    }
}
