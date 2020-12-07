using System.Collections.Generic;

[System.Serializable]
public class SceneData
{
    public List<NestData>      savedNests           = new List<NestData>();
    public TasksProgression    saveTasksProgression = new TasksProgression();
    public PlayerInventoryData savedInventoryData   = new PlayerInventoryData();
}
