using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI :MonoBehaviour, IVRInteractableObj {
    
    SteamVR_TrackedObject selectObj;
    bool drag = false;
    bool rayIn = false;
    float dis;
    Vector3 offset;

    private void Awake()
    {
        selectObj = Manager.manager.trackedObjectManager.right;
    }
    // Use this for initialization
    void Start () {
        Manager.manager.trackedObjectManager.getGripPressDown += SetDrag;
        Manager.manager.trackedObjectManager.getGripPress += Drag;
        Manager.manager.trackedObjectManager.getGripPressUp += EndDrag;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnRayIn(VRRayCastArgs rayArgs)
    {
        if (selectObj.index == rayArgs.trackedObj.index)
        {
            rayIn = true;
        }
    }

    public void OnRayOut(VRRayCastArgs rayArgs)
    {
        if (rayArgs.trackedObj.index == selectObj.index)
        {
            rayIn = false;
        }
    }

    public void OnRayStay(VRRayCastArgs rayArgs)
    {

    }

    void SetDrag(SteamVR_TrackedObject trackedObj)
    {
        if (rayIn && trackedObj.index == selectObj.index)
        {
            dis = Vector3.Distance(trackedObj.transform.position, trackedObj.GetComponent<EmitRay>().endPosition);
            offset = trackedObj.GetComponent<EmitRay>().endPosition - transform.position;
            drag = true;
        }
    }

    void Drag(SteamVR_TrackedObject trackedObj)
    {
        if (trackedObj.index == selectObj.index && drag)
        {
            Vector3 endP = trackedObj.transform.position + trackedObj.transform.forward * dis;
            transform.position = endP - offset;
        }
    }

    void EndDrag(SteamVR_TrackedObject trackedObj)
    {
        if (trackedObj.index == selectObj.index)
        {
            drag = false;
        }
    }
}
