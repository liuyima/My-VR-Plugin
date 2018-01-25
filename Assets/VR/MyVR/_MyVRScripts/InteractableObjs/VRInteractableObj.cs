
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/*
    *功能：可操作的物体继承此类 
    */
public enum InteractableObjType { VRUI, UsableObj, BeWatchedObj }
public abstract class VRInteractableObj : MonoBehaviour, IVRInteractableObj
{
    [HideInInspector]
    public InteractableObjType myObjType;
    protected SteamVR_TrackedObject.EIndex selectIndex = SteamVR_TrackedObject.EIndex.None;

    public delegate void VRInteractableHandler();
    public event VRInteractableHandler rayInEvent, rayStayEvent, rayOutEvent;

    public bool interactable { get { return _interactable; } set { _interactable = value; } }
    protected bool _interactable = true;

    public bool rayIn { get; protected set; }

    protected void Awake()
    {
        AwakeInitialize();
    }

    public void OnRayIn(VRRayCastArgs rayArgs)
    {
        if (selectIndex == SteamVR_TrackedObject.EIndex.None)
        {
            selectIndex = rayArgs.trackedObj.index;
            rayIn = true;
            RayInAction();
        }
    }

    public void OnRayOut(VRRayCastArgs rayArgs)
    {
        if (selectIndex == rayArgs.trackedObj.index)
        {
            rayIn = false;
            selectIndex = SteamVR_TrackedObject.EIndex.None;
            RayOutAction();
        }
    }

    public void OnRayStay(VRRayCastArgs rayArgs)
    {
        if (selectIndex == rayArgs.trackedObj.index)
        {
            RayStayAction();
        }
    }

    public abstract void AwakeInitialize();
    protected abstract void RayInAction();
    protected abstract void RayStayAction();
    protected abstract void RayOutAction();
}