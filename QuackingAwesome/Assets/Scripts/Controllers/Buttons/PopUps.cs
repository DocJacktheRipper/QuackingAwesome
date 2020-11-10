using System.Collections;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject player;
    
    public GameObject controls; // To disabling movement controls
    
    // The tutorial advices to be displayed
    public GameObject objective; // Objective explanation
    public GameObject energyExplanation; // Energy explanation when a first pea is ate
    public GameObject stickExplanation; // Stick explanation when a first stick is carried
    public GameObject fewSticksExplanation; // When it take too long to find the fist a stick

    public int displayTime; // How long the tutorial advices should be displayed
    public int stickResearchTime; // Max research time for stick before displaying the advice
    private int _time;
    
    // checking if popup is displayed more than once
    private bool _peaEaten;
    private bool _stickGotten;

    private Inventory.EnergyInventory _energyInventory;
    private Inventory.StickInventory _stickInventory;
    private int _numberOfSticks;

    private GameObject _currentPopUp;

    private void Awake()
    {
        // Fetch the player inventory
        _energyInventory = player.GetComponent<Inventory.EnergyInventory>();
        _stickInventory = player.GetComponent<Inventory.StickInventory>();
        
        // Display first advice
        StartCoroutine(DisplayTutorialAdvice(
            objective
            //, displayTime
            //, true
            //, false
            ));
        
        // Time spend searching for a stick
        _numberOfSticks = _stickInventory.numberOfSticks;
        _time = stickResearchTime;
        StartCoroutine(displayBeaverStoleSticks());
    }
    
    // Coroutine to start to display the advice given in argument
    private IEnumerator DisplayTutorialAdvice(
        GameObject popUp 
        //, int displayTime
        //, bool pauseGame 
        //, bool activeControls
        )
    {
        if (_currentPopUp) _currentPopUp.SetActive(false);
        _currentPopUp = popUp;
        popUp.SetActive(true);
        //controls.SetActive(activeControls); 
        //Time.timeScale = pauseGame ? 0f : 1f; 
        yield return new WaitForSecondsRealtime(displayTime);
        popUp.SetActive(false);
        //controls.SetActive(true); 
        //Time.timeScale = 1f; 
        _currentPopUp = null;
    }
    
    // Count down before give the stick disapparence explanation
    private IEnumerator displayBeaverStoleSticks()
    {
        while(_time>0){
            _time--;
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(DisplayTutorialAdvice(fewSticksExplanation));
    }

    private void Update()
    {
        // Peas
        if (!_peaEaten && 
            _energyInventory.energy > 0)
        {
            StartCoroutine(DisplayTutorialAdvice(
                energyExplanation
                //, displayTime
                //, false
                //, true
                ));
            _peaEaten = true;
        }
        
        // Sticks
        if (!_stickGotten && 
            _numberOfSticks == 1)
        {
            StartCoroutine(DisplayTutorialAdvice(
                stickExplanation
                //, displayTime
                //, false
                //, true
                ));
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
