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
                Debug.Log("Editor button pressed");
            });
            saveButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SetUIScreen(UIManager.UIScreen.SaveGame);
                Debug.Log("Save button pressed");
            });
            loadButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SetUIScreen(UIManager.UIScreen.LoadGame);
                Debug.Log("Load button pressed");
            });
        }
    }
}