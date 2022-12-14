using System;
using _Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UISaveLevel : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button newLevelButton;
        
        //parent transform for the buttons
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject buttonPrefab;
        private int _listNumber = 0;

        private void Awake()
        {
            SaveAndLoadManager.levelsNamesLoadedEvent += OnLevelsNameChanged;
        }

        private void Start()
        {
            backButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SetUIScreen(UIManager.UIScreen.MainMenu);
            });
            newLevelButton.onClick.AddListener(() =>
            {
                SaveAndLoadManager.Instance.SaveCurrentLevel();
            });
        }

        private void OnLevelsNameChanged()
        {
            //create a UI button for each name adding an onclick event with that loads the level on the specific name
            var list = SaveAndLoadManager.Instance.GetCurrentLevelNames();
            
            for (int i = _listNumber; i < list.Count; i++)
            {
                SpawnsSaveGameUIButton(list[i]);
            }
        }

        private void SpawnsSaveGameUIButton(string levelName)
        {
            var button = Instantiate(buttonPrefab, content.transform);
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveAndLoadManager.Instance.SaveLevel(levelName);
            });
            button.GetComponentInChildren<TextMeshProUGUI>().text = levelName;
            _listNumber++;
        }
    }
}