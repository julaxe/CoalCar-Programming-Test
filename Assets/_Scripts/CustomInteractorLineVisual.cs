using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts
{
    public class CustomInteractorLineVisual : XRInteractorLineVisual
    {
        protected void OnDestroy()
        {
            if (base.reticle)
            {
                Destroy(base.reticle);
            }
        }
        
    }
}