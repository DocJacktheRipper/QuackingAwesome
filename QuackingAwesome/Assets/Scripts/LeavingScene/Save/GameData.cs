[System.Serializable]
public class GameData
{
    public int higherSceneCompletedID = 0;
    public int currentSceneID         = 0;
    public SceneData[]         savedScenes; //              = new SceneData[4];             
    public UpgradesProgression savedUpgradesProgression = new UpgradesProgression();
    public MillstonesData      savedMillstonesData      = new MillstonesData();
}
