using UnityEngine;
using UnityEngine.UI;

public class DucklingShow : MonoBehaviour
{
    private GameObject duck;

    private Text _text;
    private DucklingsInventory _ducklingsInventory;

    private int _count;

    void Start()
    {
        duck = GameObject.Find("Duck");
        _text = GetComponent<Text>();
        _ducklingsInventory = duck.GetComponent<DucklingsInventory>();
    }

    void Update()
    {
        var temp = _count;

        _count = _ducklingsInventory.DucklingCount;

        if (_count != temp)
        {
            _text.text = _count.ToString();
        }
    }
}
