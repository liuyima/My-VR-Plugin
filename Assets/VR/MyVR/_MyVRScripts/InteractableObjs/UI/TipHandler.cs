using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipHandler :MonoBehaviour, IVRInteractableObj
{
    [HideInInspector]
    public string information;
    void Awake()
    {
    }

    public void OnRayIn(VRRayCastArgs rayArgs)
    {
        if (rayArgs.trackedObj.index == Manager.manager.trackedObjectManager.right.index)
        {
            Manager.manager.uiManager.informationSign.ShowSign(information,this.gameObject);
        }
    }

    public void OnRayOut(VRRayCastArgs rayArgs)
    {
        if (rayArgs.trackedObj.index == Manager.manager.trackedObjectManager.right.index)
        {
            Manager.manager.uiManager.informationSign.HideSign(this.gameObject);
        }
    }

    public void OnRayStay(VRRayCastArgs rayArgs)
    {
        if (rayArgs.trackedObj.index == Manager.manager.trackedObjectManager.right.index)
        {

        }
    }
}
