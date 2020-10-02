using Inventory;
using UnityEngine;
using UnityEngine.UI;

public class PeaBar : MonoBehaviour
{
    public GameObject duck;
    private EnergyInventory _inventory;
    private Text _text;

    private int _energy;
    
    void Start()
    {
        _inventory = duck.GetComponent<EnergyInventory>();
        _text = GetComponent<Text>();
    }

    void Update()
    {
        int tempEnergy = _energy;

        _energy = (int) _inventory.energy;

        if (_energy != tempEnergy)
        {
            _text.text = _energy.ToString();
        }
    }
}
