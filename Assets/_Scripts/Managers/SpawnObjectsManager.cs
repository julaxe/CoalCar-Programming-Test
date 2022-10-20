using System;
using System.Collections.Generic;
using _Scripts.ScriptableObjects;
using _Scripts.Units;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Managers
{
    public class SpawnObjectsManager : MonoBehaviour
    {
        //Singleton
        public static SpawnObjectsManager Instance;
        
        // all the objects that can be spawned
       public List<SpawnableObject> availableObjects;
       
       //reference to Parent class
       public GameObject parentObject;

       private List<GameObject> _currentSpawnableObjectsInGame;

       private SpawnableObject _currentSpawnableObject;

       public UnityEvent currentSpawnableChangedEvent;

       private void Awake()
       {
           if (Instance != null)
           {
               Destroy(this.gameObject);
               return;
           }
           Instance = this;
           _currentSpawnableObjectsInGame = new List<GameObject>();
       }

       private void Start()
       {
           if(availableObjects != null && availableObjects.Count > 0)
                SetCurrentSpawnableObject(availableObjects[0]);
       }

       public void SpawnObject(Transform trans)
       {
           var temp = Instantiate(_currentSpawnableObject.prefab, trans.position, trans.rotation, parentObject.transform);
           _currentSpawnableObjectsInGame.Add(temp);
       }

       public void SpawnObjectWithSavedData(Spawnable.SaveData saveData)
       {
           foreach (var spawnable in availableObjects)
           {
               if (spawnable.id == saveData.objectId)
               {
                   var temp = Instantiate(_currentSpawnableObject.prefab, parentObject.transform);
                   temp.GetComponent<Spawnable>().LoadSavedData(saveData, spawnable);
                   _currentSpawnableObjectsInGame.Add(temp);
                   return;
               }
           }
       }

       public void ClearCurrentSpawnableObjects()
       {
           if (_currentSpawnableObjectsInGame.Count == 0) return;
           
           foreach (var obj in _currentSpawnableObjectsInGame)
           {
               Destroy(obj);
           }
           _currentSpawnableObjectsInGame.Clear();
       }

       public void DestroyObject(GameObject spawnable)
       {
           if (!_currentSpawnableObjectsInGame.Contains(spawnable)) return;
           _currentSpawnableObjectsInGame.Remove(spawnable);
           Destroy(spawnable);
       }

       public void SetCurrentSpawnableObject(SpawnableObject spawnable)
       {
           _currentSpawnableObject = spawnable;
           currentSpawnableChangedEvent?.Invoke();
       }

       public SpawnableObject GetCurrentSpawnableObject()
       {
           return _currentSpawnableObject;
       }

       public List<GameObject> GetCurrentSpawnableObjectsInGame() => _currentSpawnableObjectsInGame;





    }
}