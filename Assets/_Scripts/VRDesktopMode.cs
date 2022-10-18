using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRDesktopMode : MonoBehaviour
{
    void Awake()
    {
        if (XRSettings.isDeviceActive && XRSettings.loadedDeviceName != "MockHMD Display")
        {
            this.gameObject.SetActive(false);
        }
    }
}
