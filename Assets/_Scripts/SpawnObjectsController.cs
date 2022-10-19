using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts
{
    public class SpawnObjectsController : MonoBehaviour
    {

        public XRRayInteractor rayInteractor;
        public InputActionProperty inputActionProperty;

        public GameObject previewObject;
        public GameObject realObject;
        
        private InputAction _spawnInputAction;
        private GameObject _previewObject;

        private void Start()
        {
            rayInteractor.enabled = false;

            if (inputActionProperty.reference == null)
            {
                Debug.LogWarning("Teleport action is not set up");
                return;
            }

            _spawnInputAction = inputActionProperty.action;
            _spawnInputAction.performed += SpawnPreviewObject;
            _spawnInputAction.canceled += SpawnRealObject;
        }

        private void Update()
        {
            if (_previewObject && _previewObject.activeSelf)
            {
                //update preview if it's active
                if (rayInteractor.TryGetCurrent3DRaycastHit(out var raycastHit))
                {
                    _previewObject.transform.position =
                        raycastHit.point + raycastHit.normal * (_previewObject.transform.localScale.y * 0.5f);
                }
            }
        }

        private void SpawnPreviewObject(InputAction.CallbackContext context)
        {
            rayInteractor.enabled = true;
            //get new previewObject
            //compare previewObject with the old one
            //if they are different then spawn a new one and delete the old one
            if(!_previewObject)
                _previewObject = Instantiate(previewObject);
            _previewObject.SetActive(true);
        }
        
        private void SpawnRealObject(InputAction.CallbackContext context)
        {
            
            //hide preview
            _previewObject.SetActive(false);
            //check if it's still valid
            //spawn the object
            rayInteractor.enabled = false;
            
        }
    }
}