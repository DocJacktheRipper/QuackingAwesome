#define VERBOSE

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.sitepoint.com/saving-data-between-scenes-in-unity/

// https://www.sitepoint.com/saving-and-loading-player-game-data-in-unity/
// https://www.red-gate.com/simple-talk/dotnet/c-programming/saving-game-data-with-unity/
// https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934
namespace LeavingScene.Save
{
    public class GlobalControl : MonoBehaviour
    {
        public static GlobalControl Instance;
        public GameData savedGame = new GameData();
        void Awake () {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
#if VERBOSE
            Debug.Log("Scene index " + SceneManager.GetActiveScene().buildIndex);   
            Debug.Log("Number of scenes " + sceneCount);
#endif
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy (gameObject);
            }
        
            savedGame.savedScenes = new SceneData[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                savedGame.savedScenes[i] = new SceneData();
            }
            LoadData();
        }
    
        public bool IsSceneBeingLoaded = false;

        public void SaveData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Create (Application.persistentDataPath + "/savedGame.gd");

            formatter.Serialize(saveFile, savedGame);

            saveFile.Close();
        }

        public void LoadData()
        {
            if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream saveFile = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);

                savedGame = (GameData) formatter.Deserialize(saveFile);

                saveFile.Close();
            }
        }

        public void ResetData()
        {
            savedGame = new GameData();
        }

        private void OnDestroy()
        {
            SaveData();
        }
    }
}
