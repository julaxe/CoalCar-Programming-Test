using System;
using _Scripts.Managers;
using _Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UIEditor : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject uISpawnablePrefab;
        [SerializeField] private Image currentObjectImage;

        private void Start()
        {
            backButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SetUIScreen(UIManager.UIScreen.MainMenu);
            });

            SpawnObjectsManager.Instance.currentSpawnableChangedEvent.AddListener(OnSpawnableChanged);
            PopulateContent();
        }

        private void PopulateContent()
        {
            var listOfSpawnables = SpawnObjectsManager.Instance.availableObjects;

            foreach (var spawnable in listOfSpawnables)
            {
                SpawnUISpawnable(spawnable);
            }
        }

        private void SpawnUISpawnable(SpawnableObject spawnable)
        {
            var spawnableUI = Instantiate(uISpawnablePrefab, content.transform);
            spawnableUI.GetComponent<UISpawnable>().SetSpawnableInfo(spawnable);
        }

        private void OnSpawnableChanged()
        {
            currentObjectImage.sprite = SpawnObjectsManager.Instance.GetCurrentSpawnableObject().sprite;
        }

       
    }
}