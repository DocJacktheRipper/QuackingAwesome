using UnityEngine;
using UnityEngine.UI;

using LeavingScene.Save;

public class EggsHatched : MonoBehaviour
{
    private int state = 0;
    private int _progression;
    private readonly int[] _goals = {5, 10, 30};

    public Text explanation;
    public Text printedProgression;

    public ProgressBar progressBar;
    
    public GameObject levelStars;
    
    // Start is called before the first frame update
    void Start()
    {
        explanation.text = "Hatch " + _goals[state] + " ducklings";
        
        progressBar.BarValue = _progression * 100 / _goals[state];        
        printedProgression.text = _progression.ToString();
        
        levelStars.transform.GetChild(state).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (state >= _goals.Length)
            return;
        
        _progression = GlobalControl.Instance.savedGame.savedMillstonesData.hatchEggs;
        if (_progression >= _goals[state])
        {
            if (state++ == _goals.Length)
                explanation.text = "Congratulations, you have ended this millstone!";
            
            levelStars.transform.GetChild(state).gameObject.SetActive(true);
        }
        else
        {
                explanation.text = "Hatch " + _goals[state] + " ducklings";
                progressBar.BarValue = _progression * 100 / _goals[state];
        }
        printedProgression.text = _progression.ToString();
    }
}