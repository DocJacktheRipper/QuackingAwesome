using System.Collections.Generic;
using UnityEngine;

using Nest;

public class SticksToRock : Task
{
    private List<NestBuilding> _nests;
    public SticksToRock(GameObject nestsParent)
    {
        description = "Bring sticks to a rock";
        
        GameObject _nestsParent = nestsParent;
        
        _nests = new List<NestBuilding>(_nestsParent.transform.childCount);
        for (int i = 0; i < _nestsParent.transform.childCount; i++)
        {
            _nests.Add(_nestsParent.transform.GetChild(i).GetComponent<NestBuilding>());
        }
    }

    public override bool Update()
    {
        int newProgression = 0;
        foreach (var nest in _nests)
            newProgression += nest.numberOfSticks;

        progression = newProgression;
        return IsCompleted();
    }
}

public class BuildAllNests : Task
{
    private GameObject _nestsParent;
    private List<NestBuilding> _nests;

    public BuildAllNests(GameObject nestsParent)
    {
        description = "Build all the nests";
        
        _nestsParent = nestsParent;
        
        goal = _nestsParent.transform.childCount;
        
        _nests = new List<NestBuilding>(goal);
        for (int i = 0; i < goal; i++)
        {
            _nests.Add(_nestsParent.transform.GetChild(i).GetComponent<NestBuilding>());
        }
    }

    public override bool Update()
    {
        int newProgression = 0;
        foreach (var nest in _nests)
            if (nest.NestIsFinished) newProgression++;

        progression = newProgression;
        return IsCompleted();
    }
}
