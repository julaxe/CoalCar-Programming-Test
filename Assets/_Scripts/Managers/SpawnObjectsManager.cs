using System;
using System.Collections.Generic;
using _Scripts.ScriptableObjects;
using UnityEngine;

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

       public void SpawnObject(GameObject spawnable, Transform trans)
       {
           var temp = Instantiate(spawnable, trans.position, trans.rotation, parentObject.transform);
           _currentSpawnableObjectsInGame.Add(temp);
       }

       public void DestroyObject(GameObject spawnable)
       {
           if (!_currentSpawnableObjectsInGame.Contains(spawnable)) return;
           _currentSpawnableObjectsInGame.Remove(spawnable);
           Destroy(spawnable);
       }
       


    }
}