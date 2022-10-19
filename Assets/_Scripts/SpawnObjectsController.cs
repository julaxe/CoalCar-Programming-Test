using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts
{
    public class SpawnObjectsController : MonoBehaviour
    {
        public enum ControllerType
        {
            RightHand,
            LeftHand
        }

        public ControllerType targetController;

        public InputActionAsset inputAction;

        public XRRayInteractor rayInteractor;
        

        private InputAction _thumbstickInputAction;
    }
}