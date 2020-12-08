#define VERBOSE

using UnityEngine;

[System.Serializable]
public class Task
{
    public string description = "";

    public int goal = 0;
    public int progression = 0;
    public bool isCompleted = false;

    public bool IsCompleted()
    {
        isCompleted = (progression >= goal);
        #if (VERBOSE) 
            if (isCompleted) Debug.Log("tasks '" + description + "' is completed");
        #endif
        return isCompleted;
    }

    public virtual bool UpdateProgression()
    {
        return IsCompleted();
    }
}
