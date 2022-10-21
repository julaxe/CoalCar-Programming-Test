using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Units;
using UnityEngine;

namespace _Scripts.Managers
{
    public class SaveAndLoadManager : MonoBehaviour
    {
        public static SaveAndLoadManager Instance;

        private List<string> _levelNames;

        public static Action levelsNamesLoadedEvent;
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
            var listOfObjects = JsonHelper.FromJson<Spawnable.SaveData>(levelData).ToList();
            
            //create every single object in the game with the saved data
            foreach (var obj in listOfObjects)
            {
                var savedData = obj;
                SpawnObjectsManager.Instance.SpawnObjectWithSavedData(savedData);
            }
        }

        public void LoadLevelNames()
        {
            if (!PlayerPrefs.HasKey("levels")) return;
            var list = PlayerPrefs.GetString("levels");
            
            _levelNames = JsonHelper.FromJson<string>(list).ToList();
            levelsNamesLoadedEvent?.Invoke();
        }

        public void SaveLevel(string levelName)
        {
            //get dictionary
            var currentObjectsInGame = SpawnObjectsManager.Instance.GetCurrentSpawnableObjectsInGame();
            
            //create new dictionary with saved data
            var listOfObjects = new List<Spawnable.SaveData>();
            
            foreach (var spawnable in currentObjectsInGame)
            {
                listOfObjects.Add(spawnable.GetComponent<Spawnable>().GetSaveData());
            }
            

            //saved serialized dictionary
            var jsonList = JsonHelper.ToJson<Spawnable.SaveData>(listOfObjects.ToArray());
            PlayerPrefs.SetString(levelName, jsonList);
            
            Debug.Log(levelName + " saved!");
            
            //Add another level to the list
            if (_levelNames.Contains(levelName)) return;
            _levelNames.Add(levelName);
            var jsonNames = JsonHelper.ToJson<string>(_levelNames.ToArray());
            PlayerPrefs.SetString("levels",jsonNames);
            levelsNamesLoadedEvent?.Invoke();
        }
        
        public void DeleteData()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Data deleted");
        }
        public List<string> GetCurrentLevelNames() => _levelNames;

    }
    
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}