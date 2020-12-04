﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int id = 0;
    public NestData savedNest = new NestData();
    public TasksProgression saveTasksProgression = new TasksProgression();
}
