using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodViewPoint : VRInteractableObj {

    public static GodViewPoint currentPoint;
    SpriteRenderer sprite;
	
    public void SelectThisPoint()
    {
        if (currentPoint)
        {
            currentPoint.Deselect();
        }
        Manager.manager.vrMove.ChangePlayerPos(transform.position);
        Manager.manager.vrMove.ChangeHeadDir(transform.parent.forward);
        currentPoint = this;
        sprite.enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    public void Deselect()
    {
        sprite.enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    protected override void RayInAction()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    protected override void RayOutAction()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected override void RayStayAction()
    {
    }
    protected void Select(SteamVR_TrackedObject trackedObj)
    {
        if (rayIn && trackedObj.index == Manager.manager.trackedObjectManager.right.index)
        {
            Manager.manager.audioManager.PlaySelectAudio();
            SelectThisPoint();
        }
    }

    public override void AwakeInitialize()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
}
