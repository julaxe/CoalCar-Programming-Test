using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class MenuController : MonoBehaviour
    {
        public InputActionProperty inputActionProperty;
        [SerializeField] private Canvas canvasMenu;
        
        private InputAction _menuInputAction;
        
        private void Start()
        {
            _menuInputAction = inputActionProperty.action;

            _menuInputAction.performed += OnMenuPressed;

        }

        private void OnMenuPressed(InputAction.CallbackContext context)
        {
            canvasMenu.enabled = !canvasMenu.enabled;
        }
        
        
    }
}