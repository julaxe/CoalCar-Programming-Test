using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private Button editorButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;

        private void Start()
        {
            editorButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SetUIScreen(UIManager.UIScreen.Editor);
            });
            saveButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SetUIScreen(UIManager.UIScreen.SaveGame);
                SaveAndLoadManager.Instance.LoadLevelNames();
            });
            loadButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SetUIScreen(UIManager.UIScreen.LoadGame);
                SaveAndLoadManager.Instance.LoadLevelNames();
            });
        }
    }
}