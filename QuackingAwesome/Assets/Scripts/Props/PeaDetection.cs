using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name == "Starting")
        {
            PopUps popups = GameObject.FindGameObjectWithTag("PopUpCanvas").GetComponent<PopUps>();
            if (other.gameObject.tag == "Player" && popups.p2 == true)
            {
                popups.popUp2();
                return true;
            }
        }
        return false;
    }
}
