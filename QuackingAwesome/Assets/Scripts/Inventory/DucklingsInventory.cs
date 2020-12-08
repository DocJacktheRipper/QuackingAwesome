using LeavingScene.Save;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DucklingsInventory : MonoBehaviour
{
    public int ducklingCount;
    private int _sceneID;

    private void Start()
    {
        _sceneID = SceneManager.GetActiveScene().buildIndex;
        ducklingCount = GlobalControl.Instance.savedGame.
            savedScenes[_sceneID].
            savedInventoryData.
            ducklings;
    }

    public void RemoveDucklings(int number)
    {
        ducklingCount -= number;
        if (ducklingCount < 0)
        {
            ducklingCount = 0;
        }
    }

    public void AddDucklings(int number)
    {
        ducklingCount += number;
    }

    private void OnDestroy()
    {
        GlobalControl.Instance.savedGame.
            savedScenes[_sceneID].
            savedInventoryData.
            ducklings = ducklingCount;
    }
}
