using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts
{
    public class GrabController : MonoBehaviour
    {

        public XRRayInteractor rayInteractor;
        public SpawnObjectsController spawnObjectsController;
        public InputActionProperty inputActionProperty;

        private bool _isGrabbing;
        private InputAction _deleteObjectAction;

        private void Start()
        {
            _deleteObjectAction = inputActionProperty.action;
            _deleteObjectAction.performed += OnDeleteObject;
        }

        public void OnGrabbed()
        {
            if (spawnObjectsController.isSpawning) return;
            _isGrabbing = true;
        }
        public void OnReleased()
        {
            if (spawnObjectsController.isSpawning) return;
            _isGrabbing = false;
        }

        private void OnDeleteObject(InputAction.CallbackContext context)
        {
            if (!_isGrabbing) return;
            if (rayInteractor.TryGetCurrent3DRaycastHit(out var raycastHit))
            {
                SpawnObjectsManager.Instance.DestroyObject(raycastHit.transform.gameObject);
            }
        }

        
    }
}