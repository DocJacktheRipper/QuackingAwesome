using System;
using System.Collections;
using System.Collections.Generic;
using Nest;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    public bool tasksCompleted;

    public class task
    {
        public bool isCompleted = false;
        public int goal = 0;
        public int progression = 0;
    }


    public Dictionary<string, task> levelTasks = new Dictionary<string, task>()
    {
        {"buildNests", new task()}
    };

    public int numTaskCompleted;

    public GameObject nestsParent;
    private List<NestBuilding> _nests;

    // Start is called before the first frame update
    void Start()
    {
        levelTasks["buildNests"].goal = nestsParent.transform.childCount;

        for (int i = 0; i < levelTasks["buildNests"].goal; i++)
        {
            _nests[i] = nestsParent.transform.GetChild(i).GetComponent<NestBuilding>();
        }
    }

    private int UpdateNestsCompleted()
    {
        levelTasks["buildNests"].progression = 0;
        foreach (var nest in _nests)
        {
            if (nest.NestIsFinished) levelTasks["buildNests"].progression++;
        }

        return levelTasks["buildNests"].progression;
    }

    private bool TaskIsCompleted(int progression, int goal)
    {
        return (progression == goal);
    }

    // Update is called once per frame
    void Update()
    {
        if (TaskIsCompleted(UpdateNestsCompleted(), levelTasks["buildNests"].goal)) ;
        TaskIsCompleted(levelTasks.Count, numTaskCompleted);
    }

    private void OnDestroy()
    {
        GlobalControl.Instance.savedPlayerData.currentScene.saveTasksProgression.tasksAreCompleted = TaskIsCompleted(levelTasks.Count, numTaskCompleted);
    }
}
