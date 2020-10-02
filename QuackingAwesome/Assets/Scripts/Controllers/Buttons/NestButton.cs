using Props;
using UnityEngine;
using UnityEngine.UI;

public class NestButton : MonoBehaviour
{
    public GameObject nest;
    private NestBuilding _nestInventory;
    
    void Start()
    {
        _nestInventory = nest.GetComponent<NestBuilding>();
    }

    public void ActivateNestButton()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateNestButton()
    {
        gameObject.SetActive(false);
    }

    void ExpandNestMenu()
    {
        
    }
}
