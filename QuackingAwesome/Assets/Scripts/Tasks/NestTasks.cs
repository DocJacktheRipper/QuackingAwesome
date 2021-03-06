﻿using System.Collections.Generic;
using UnityEngine;

using Nest;

public class SticksToRock : Task, ISerializationCallbackReceiver
{
    private List<NestBuilding> _nests;
    
    public void OnBeforeSerialize() {}
    public void OnAfterDeserialize() {}
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

    public override bool UpdateProgression()
    {
        int newProgression = 0;
        foreach (var nest in _nests)
            newProgression += nest.GETNumberOfSticks();

        progression = newProgression;
        return IsCompleted();
    }
}

public class BuildAllNests : Task, ISerializationCallbackReceiver
{
    private GameObject _nestsParent;
    private List<NestBuilding> _nests;
    
    public void OnBeforeSerialize() {}
    public void OnAfterDeserialize() {}

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

    public override bool UpdateProgression()
    {
        int newProgression = 0;
        foreach (var nest in _nests)
            if (nest.GETNestFinished()) newProgression++;

        progression = newProgression;
        return IsCompleted();
    }
}
