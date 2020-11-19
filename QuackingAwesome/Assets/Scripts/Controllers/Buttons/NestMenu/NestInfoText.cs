using UnityEngine.UI;
using UnityEngine;

using Nest.NestMenu;

public class NestInfoText : MonoBehaviour
{
    public LayAndHatchEgg nest;

    private Text _text;
    private int _currentNumEggs;
    private string _maxEggs;

    void Start()
    {
        _text           = GetComponent<Text>();
        _currentNumEggs = nest.getNumEggs();
        _maxEggs        = nest.maxEggsInNest.ToString();
        _text.text      = "You have " + _currentNumEggs.ToString() + " / " + _maxEggs + " eggs in your nest waiting to be hatched.";
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentNumEggs != nest.getNumEggs())
        {
            _currentNumEggs = nest.getNumEggs();
            _text.text      = "You have " + _currentNumEggs.ToString() + " / " + _maxEggs + " eggs in your nest waiting to be hatched.";
        }
    }
}
