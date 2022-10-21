using System;
using _Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UILoadLevel : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button refreshButton;
        
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
            refreshButton.onClick.AddListener(() =>
            {
                SaveAndLoadManager.Instance.LoadLevelNames();
            });
        }

        private void OnLevelsNameChanged()
        {
            //create a UI button for each name adding an onclick event with that loads the level on the specific name
            var list = SaveAndLoadManager.Instance.GetCurrentLevelNames();
            
            for (int i = _listNumber; i < list.Count; i++)
            {
                SpawnsLoadGameUIButton(list[i]);
            }
        }

        private void SpawnsLoadGameUIButton(string levelName)
        {
            var button = Instantiate(buttonPrefab, content.transform);
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveAndLoadManager.Instance.LoadLevel(levelName);
            });
            button.GetComponentInChildren<TextMeshProUGUI>().text = levelName;
            _listNumber++;
        }
    }
}