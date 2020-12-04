using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// https://www.sitepoint.com/saving-data-between-scenes-in-unity/

// https://www.sitepoint.com/saving-and-loading-player-game-data-in-unity/
// https://www.red-gate.com/simple-talk/dotnet/c-programming/saving-game-data-with-unity/
// https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934
public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public PlayerData savedPlayerData = new PlayerData();
    void Awake () {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }
    
    public bool IsSceneBeingLoaded = false;

    public void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create (Application.persistentDataPath + "/savedGame.gd");

        formatter.Serialize(saveFile, savedPlayerData);

        saveFile.Close();
    }

    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);

        savedPlayerData = (PlayerData)formatter.Deserialize(saveFile);
        
        saveFile.Close();
    }
    
    public void ResetData()
    {
        savedPlayerData = new PlayerData();
    }

    private void OnDestroy()
    {
        SaveData();
    }
}
