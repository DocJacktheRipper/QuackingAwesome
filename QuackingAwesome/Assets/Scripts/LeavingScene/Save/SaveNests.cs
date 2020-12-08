#define VERBOSE

using System.Collections.Generic;
using System.Linq;
using LeavingScene.Save;
using UnityEngine;
using UnityEngine.SceneManagement;

using Nest;

public class SaveNests : MonoBehaviour
{
    private List<NestData> _savedNests;
    private int _numberOfNests;

    // Start is called before the first frame update
    void Start()
    {
        _savedNests = GlobalControl.Instance.savedGame
            .savedScenes[SceneManager.GetActiveScene().buildIndex]
            .savedNests;
        
        // load data
        _numberOfNests = gameObject.transform.childCount;
        if (!_savedNests.Any())
        {
            for (int i = 0; i < _numberOfNests; i++)
                _savedNests.Add(new NestData());
        }
        
        for (int i = 0; i < _numberOfNests; i++)
        {
            gameObject.transform.GetChild(i)
                .GetComponent<NestBuilding>()
                .nestDataToSave = _savedNests[i];
        }
        
    }

    // saving the nest state
    private void OnDestroy()
    {
        for (int i = 0; i < _numberOfNests; i++)
        {
            _savedNests[i] = 
                gameObject.transform.GetChild(i)
                .GetComponent<NestBuilding>().nestDataToSave;
        }
    }
}