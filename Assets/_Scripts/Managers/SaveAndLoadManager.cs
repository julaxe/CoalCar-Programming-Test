using System;
using System.Collections.Generic;
using _Scripts.Units;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Managers
{
    public class SaveAndLoadManager : MonoBehaviour
    {
        public static SaveAndLoadManager Instance;

        private List<string> _levelNames;

        [HideInInspector] public UnityEvent levelsNamesLoadedEvent;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            _levelNames = new List<string>();
        }

        private void Start()
        {
            LoadLevelNames();
        }


        public void SaveCurrentLevel()
        {
            string levelName = "level " + (_levelNames.Count + 1);

            SaveLevel(levelName);
            
        }

        public void LoadLevel(string levelName)
        {
            //clear current objects
            SpawnObjectsManager.Instance.ClearCurrentSpawnableObjects();
            
            //get saved level
            if (!_levelNames.Contains(levelName)) return;
            var levelData = PlayerPrefs.GetString(levelName);
            
            //get list from level
            var listOfObjects = JsonUtility.FromJson<List<object>>(levelData);
            
            //create every single object in the game with the saved data
            foreach (var obj in listOfObjects)
            {
                var savedData = (Spawnable.SaveData)obj;
                SpawnObjectsManager.Instance.SpawnObjectWithSavedData(savedData);
            }
        }

        private void LoadLevelNames()
        {
            var list = PlayerPrefs.GetString("levels");
            _levelNames = JsonUtility.FromJson<List<string>>(list);
            levelsNamesLoadedEvent?.Invoke();
        }

        private void SaveLevel(string levelName)
        {
            //get dictionary
            var currentObjectsInGame = SpawnObjectsManager.Instance.GetCurrentSpawnableObjectsInGame();
            
            //create new dictionary with saved data
            var listOfObjects = new List<object>();
            
            foreach (var spawnable in currentObjectsInGame)
            {
                listOfObjects.Add(spawnable.GetComponent<Spawnable>().GetSaveData());
            }
            

            //saved serialized dictionary
            PlayerPrefs.SetString(levelName, JsonUtility.ToJson(listOfObjects));
            
            //Add another level to the list
            _levelNames.Add(levelName);
            PlayerPrefs.SetString("levels", JsonUtility.ToJson(_levelNames));
            levelsNamesLoadedEvent?.Invoke();
            
            PlayerPrefs.Save();
        }

        [Serializable]
        public struct Level
        {
            public string levelName;
            public object levelData;
        }

        public struct LevelData
        {
            public string objectId;
            public object savedData;
        }
    }
}