using UnityEngine;
using UnityEngine.SceneManagement;

public class StickDetection : MonoBehaviour
{   
    /*private void OnTriggerEnter(Collider other)
    {
        if (stickTrigger(other))
        {
            return;
        }
    }

    private bool stickTrigger(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Starting")
        {
            PopUps popups = GameObject.FindGameObjectWithTag("PopUpCanvas").GetComponent<PopUpsTutorialPrefabs>();
            if (other.gameObject.tag == "Player" && popups.p3 == true && popups.p2 == false)
            {
                popups.popUp3();
                return true;
            }
        }
        return false;
    }*/
}
