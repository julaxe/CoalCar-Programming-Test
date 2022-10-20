using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        //Singleton
        public static UIManager Instance;
        
        //reference to all the UI
        public GameObject mainMenu;
        public GameObject editor;
        public GameObject saveGame;
        public GameObject loadGame;

        private List<GameObject> _uiList;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            InitializeUiList();
        }

        private void InitializeUiList()
        {
            _uiList = new List<GameObject>
            {
                mainMenu,
                editor,
                saveGame,
                loadGame
            };
        }

        public void SetUIScreen(UIScreen uiScreen)
        {
            switch (uiScreen)
            {
                case UIScreen.MainMenu:
                    ChangeUIScreen(mainMenu);
                    break;
                case UIScreen.Editor:
                    ChangeUIScreen(editor);
                    break;
                case UIScreen.SaveGame:
                    ChangeUIScreen(saveGame);
                    break;
                case UIScreen.LoadGame:
                    ChangeUIScreen(loadGame);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiScreen), uiScreen, null);
            }
        }

        private void ChangeUIScreen(GameObject ui)
        {
            foreach (var page in _uiList)
            {
                page.SetActive(ui == page);
            }
        }

        public enum UIScreen
        {
            MainMenu,
            Editor,
            SaveGame,
            LoadGame
        }
    }
}