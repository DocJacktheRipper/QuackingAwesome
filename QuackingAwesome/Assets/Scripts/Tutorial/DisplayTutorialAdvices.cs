using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTutorialAdvices : MonoBehaviour
{
    public GameObject player;

    // The tutorial advices to be displayed
    public GameObject tutorialAdvice; // Objective explanation
    public UpdateTutorialAdvicesText textDisplayedBehaviour;

    private readonly Dictionary<string, string> _textToDisplay = new Dictionary<string, string>()
    {
        {
            "Objective", 
            "I should bring some sticks to this rock to build my nest..."
        },
        {
            "EnergyExplanation", 
            "Peas give me more energy! \n" +
            "I may be able to lay eggs to my nest if I got more..."
        },
        {
            "StickExplanation", 
            "My first stick! \n" +
            "I should bring it to the rock to start build my nest..."
        },
        {
            "FewStickExplanation", 
            "I can't find a stick... the beaver might have stolen them! \n" +
            "Maybe I can scare him if I quack loud enough?"
        }
    };
    
    public int displayTime; // How long the tutorial advices should be displayed
    public int stickResearchTime; // Max research time for stick before displaying the advice
    private int _time;
    
    // Checking if popup is displayed more than once
    private bool _peaEaten;
    private bool _stickGotten;

    private Inventory.EnergyInventory _energyInventory;
    private Inventory.StickInventory _stickInventory;
    private int _numberOfSticks;

    private void Awake()
    {
        // Fetch the player inventory
        _energyInventory = player.GetComponent<Inventory.EnergyInventory>();
        _stickInventory = player.GetComponent<Inventory.StickInventory>();
        
        // Display first advice
        StartCoroutine(DisplayTutorialAdvice(_textToDisplay["Objective"]));
        
        // Time spend searching for a stick
        _numberOfSticks = _stickInventory.numberOfSticks;
        _time = stickResearchTime;
        StartCoroutine(displayBeaverStoleSticks());
    }
    
    // Coroutine to start to display the advice given in argument
    private IEnumerator DisplayTutorialAdvice(string advice)
    {
        textDisplayedBehaviour.UpdateText(advice);
        tutorialAdvice.SetActive(true);
        yield return new WaitForSecondsRealtime(displayTime);
        tutorialAdvice.SetActive(false);
    }
    
    // Count down before give the stick disapparence explanation
    private IEnumerator displayBeaverStoleSticks()
    {
        while(_time>0){
            _time--;
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(nameof(DisplayTutorialAdvice), _textToDisplay["FewStickExplanation"]);
    }

    private void Update()
    {
        // Peas
        if (!_peaEaten && 
            _energyInventory.energy > 0)
        {
            StartCoroutine(nameof(DisplayTutorialAdvice), _textToDisplay["EnergyExplanation"]);
            _peaEaten = true;
        }
        
        // Sticks
        if (!_stickGotten && 
            _numberOfSticks == 1)
        {
            StartCoroutine(nameof(DisplayTutorialAdvice), _textToDisplay["StickExplanation"]);
            _stickGotten = true;
        }
        
        // Beaver
        if (stickResearchTime != 0 && 
            _numberOfSticks != _stickInventory.numberOfSticks)
        {
            _time = stickResearchTime;
            _numberOfSticks = _stickInventory.numberOfSticks;
        }
    }
}
