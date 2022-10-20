using System;
using _Scripts.Managers;
using _Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UISpawnable : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private SpawnableObject _spawnableObject;

        private void Awake()
        {
            _button.onClick.AddListener(ChangeCurrentSpawnable);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ChangeCurrentSpawnable);
        }

        private void ChangeCurrentSpawnable()
        {
            if(_spawnableObject)
                SpawnObjectsManager.Instance.SetCurrentSpawnableObject(_spawnableObject);
        }

        public void SetSpawnableInfo(SpawnableObject spawnableObject)
        {
            _spawnableObject = spawnableObject;
            _image.sprite = _spawnableObject.sprite;
        }
    }
}