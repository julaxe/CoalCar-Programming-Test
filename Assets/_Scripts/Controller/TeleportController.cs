using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Vector2 = UnityEngine.Vector2;

public class TeleportController : MonoBehaviour
{

    public InputActionProperty inputActionProperty;

    public XRRayInteractor rayInteractor;

    public TeleportationProvider teleportationProvider;

    private InputAction _thumbstickInputAction;
    
    

    private void Start()
    {
        rayInteractor.enabled = false;

        if (inputActionProperty.reference == null)
        {
            Debug.LogWarning("Teleport action is not set up");
            return;
        }
        _thumbstickInputAction = inputActionProperty.action;
        inputActionProperty.action.performed += TeleportEnabled;
        inputActionProperty.action.canceled += TeleportReleased;
    }

    private void TeleportEnabled(InputAction.CallbackContext context)
    {
        if (_thumbstickInputAction.ReadValue<Vector2>() != Vector2.up) return;
        
        rayInteractor.enabled = true;
    }
    private void TeleportReleased(InputAction.CallbackContext context)
    {
        //if we are not trying to teleport so don't do anything
        if (!rayInteractor.enabled) return;
        
        //get raycast hit
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit))
        {
            TeleportRequest teleportRequest = new TeleportRequest()
            {
                destinationPosition = raycastHit.point,
            };
            teleportationProvider.QueueTeleportRequest(teleportRequest);
        }
        
        rayInteractor.enabled = false;
    }


    private void Test(InputAction.CallbackContext context)
    {
        Debug.Log("LoooL");
    }
    
    
}
