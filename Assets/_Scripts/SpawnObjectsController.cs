using System;
using _Scripts.Managers;
using _Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts
{
    public class SpawnObjectsController : MonoBehaviour
    {

        public XRRayInteractor rayInteractor;
        public InputActionProperty inputActionProperty;

        public SpawnableObject spawnableObject;
        [HideInInspector] public bool isSpawning;
        
        private InputAction _spawnInputAction;
        private GameObject _refPreviewObject;
        private MeshRenderer _refPreviewObjectMeshRenderer;

        private void Start()
        {
            rayInteractor.interactionLayers = InteractionLayerMask.GetMask("Interactable");

            if (inputActionProperty.reference == null)
            {
                Debug.LogWarning("Teleport action is not set up");
                return;
            }

            _spawnInputAction = inputActionProperty.action;
            _spawnInputAction.performed += OnSpawnInputEntered;
            _spawnInputAction.canceled += OnSpawnInputCanceled;

            //not required
            if (spawnableObject)
            {
                //initialize preview if is set in the editor
                SpawnPreviewObject(spawnableObject.previewPrefab);
            }
        }

        private void Update()
        {
            if (!_refPreviewObject || !_refPreviewObject.activeSelf) return;
            
            //update preview if it's active
            if (!rayInteractor.TryGetCurrent3DRaycastHit(out var raycastHit)) return;
            
            var offSetFromGround = raycastHit.normal * _refPreviewObjectMeshRenderer.bounds.extents.y;
            //var offSetFromGround = raycastHit.normal * (_refPreviewObject.transform.localScale.y * 0.5f);
            _refPreviewObject.transform.position =
                raycastHit.point + offSetFromGround;
        }

        public void SetSpawnableObject(SpawnableObject newSpawnObj)
        {
            if (spawnableObject == newSpawnObj) return;
            
            spawnableObject = newSpawnObj;
            
            //change preview ref
            if (_refPreviewObject)
            {
                Destroy(_refPreviewObject);
            }

            SpawnPreviewObject(spawnableObject.previewPrefab);
        }

        private void SpawnPreviewObject(GameObject preview)
        {
            _refPreviewObject = Instantiate(preview);
            _refPreviewObject.SetActive(false);
            _refPreviewObjectMeshRenderer = _refPreviewObject.GetComponent<MeshRenderer>();
        }

        private void OnSpawnInputEntered(InputAction.CallbackContext context)
        {
            //check for null references
            if (!spawnableObject || !_refPreviewObject) return;
            
            //show ray
            rayInteractor.interactionLayers = InteractionLayerMask.GetMask("Spawnable");

            //show preview
            _refPreviewObject.SetActive(true);

            isSpawning = true;
        }
        
        private void OnSpawnInputCanceled(InputAction.CallbackContext context)
        {
            isSpawning = false;
            rayInteractor.interactionLayers = InteractionLayerMask.GetMask("Interactable");
            
            //check for null references
            if (!_refPreviewObject) return;
            //hide preview
            _refPreviewObject.SetActive(false);
            
            //check if it's still valid
            if (!spawnableObject) return;
            if (rayInteractor.TryGetCurrent3DRaycastHit(out var raycastHit))
            {
                //spawn the object
                SpawnObjectsManager.Instance.SpawnObject(spawnableObject.prefab, _refPreviewObject.transform);
            }
            
        }
    }
}