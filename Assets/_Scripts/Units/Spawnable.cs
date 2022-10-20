using System;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Units
{
    public class Spawnable : MonoBehaviour
    {
        public SpawnableObject spawnableObject
        {
            get;
            private set;
        }

        public void SetSpawnableObject(SpawnableObject spawnable)
        {
            spawnableObject = spawnable;
        }

        public object GetSaveData()
        {
            var storedTransform = transform;
            var position = storedTransform.position;
            var rotation = storedTransform.rotation;
            return new SaveData()
            {
                objectId = spawnableObject.id,
                posX = position.x,
                posY = position.y,
                posZ = position.z,
                rotX = rotation.x,
                rotY = rotation.y,
                rotZ = rotation.z
            };
        }

        public void LoadSavedData(SaveData savedData, SpawnableObject spawnable)
        {
            spawnableObject = spawnable;
            transform.position = new Vector3(savedData.posX, savedData.posY, savedData.posZ);
            transform.rotation = Quaternion.Euler(savedData.rotX, savedData.rotY, savedData.rotZ);
        }

        [Serializable]
        public struct SaveData
        {
            public string objectId;
            public float posX;
            public float posY;
            public float posZ;
            public float rotX;
            public float rotY;
            public float rotZ;
        }
        
    }
}