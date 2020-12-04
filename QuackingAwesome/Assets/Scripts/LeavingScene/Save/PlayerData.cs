using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int higherSceneCompletedID = 0;
    public SceneData           currentScene             = new SceneData();
    public PlayerInventoryData savedInventoryData       = new PlayerInventoryData();
    public UpgradesProgression savedUpgradesProgression = new UpgradesProgression();
    public MillstonesData      savedMillstonesData      = new MillstonesData();

}
