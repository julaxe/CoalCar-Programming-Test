using _Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UISaveLevel : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        
        private void Start()
        {
            backButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SetUIScreen(UIManager.UIScreen.MainMenu);
            });

            SaveAndLoadManager.Instance.levelsNamesLoadedEvent.AddListener(OnLevelsNameChanged);
        }

        private void OnLevelsNameChanged()
        {
            
        }
    }
}