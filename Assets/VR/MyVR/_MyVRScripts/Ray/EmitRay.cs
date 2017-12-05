using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 手柄发射射线，VR中的交互基本上都会用到此类
/// </summary>
public struct VRRayCastArgs
{
    public RaycastHit hit;
    public SteamVR_TrackedObject trackedObj;
    public VRRayCastArgs(RaycastHit hit,SteamVR_TrackedObject trackedObj)
    {
        this.hit = hit;
        this.trackedObj = trackedObj;
    }
}
public class EmitRay : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    public Vector3 endPosition { get; private set;}
    public LayerMask castLayer;
    public Line line;
    [HideInInspector]
    public GameObject currentObj;
    [HideInInspector]
    public GameObject currentInteractableObj;
    [HideInInspector]
    public RaycastHit currentHit { get; private set; }

    [HideInInspector]
    public float castLength = 1500;
    public delegate void EmitRayHandler(bool hitCollider, RaycastHit hit);
    public event EmitRayHandler onHitCollider;

    // Use this for initialization
    protected void Start () {
        if (trackedObj == null)
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        }
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        EmitRayAndGetObj();
	}

    protected bool EmitRayAndGetObj()
    {
        bool cast = false;
        currentObj = null;
        RaycastHit hit;
        bool isHittingTerrain = false;
        Ray ray = new Ray(transform.position, transform.forward);
        Vector3 lineEndPos = transform.position + transform.forward * 200;
        if (Physics.Raycast(ray,out hit,castLength,castLayer))
        {
            if (onHitCollider != null)
            {
                onHitCollider(true,hit);
            }
            cast = true;
            currentObj = hit.collider.gameObject;//当前检测到的物体
            IVRInteractableObj iobj = currentObj.GetComponent<IVRInteractableObj>();
            GameObject castedObj = currentObj;
            if (iobj == null)
            {
                iobj = currentObj.GetComponentInParent<IVRInteractableObj>();
            }
            if (castedObj != currentInteractableObj)
            {
                if (currentInteractableObj != null)
                {
                    IVRInteractableObj[] iInteractables = currentInteractableObj.GetComponents<IVRInteractableObj>();
                    if (iInteractables.Length == 0)
                    {
                        iInteractables = currentInteractableObj.GetComponentsInParent<IVRInteractableObj>();
                    }
                    for (int i = 0; i < iInteractables.Length; i++)
                    {
                        iInteractables[i].OnRayOut(new VRRayCastArgs(hit, trackedObj));
                    }
                }
                currentInteractableObj = castedObj;
                if (currentInteractableObj != null)
                {
                    IVRInteractableObj[] iobjs = currentInteractableObj.GetComponents<IVRInteractableObj>();
                    if (iobjs.Length == 0)
                    {
                        iobjs = currentInteractableObj.GetComponentsInParent<IVRInteractableObj>();
                    }
                    for (int i = 0; i < iobjs.Length; i++)
                    {
                        iobjs[i].OnRayIn(new VRRayCastArgs(hit, trackedObj));
                    }
                }
            }
            else if (currentInteractableObj != null)
            {
                IVRInteractableObj[] iobjs = currentInteractableObj.GetComponents<IVRInteractableObj>();
                if (iobjs.Length == 0)
                {
                    iobjs = currentInteractableObj.GetComponentsInParent<IVRInteractableObj>();
                }
                for (int i = 0; i < iobjs.Length; i++)
                {
                    iobjs[i].OnRayStay(new VRRayCastArgs(hit, trackedObj));
                }
            }
            lineEndPos = hit.point;

            isHittingTerrain = hit.collider.tag == Manager.manager.vrMove.currentMovableTag;
        }
        else 
        {
            if (onHitCollider != null)
            {
                onHitCollider(false, new RaycastHit());
            }
            if (currentInteractableObj != null)
            {
                IVRInteractableObj[] iobjs = currentInteractableObj.GetComponents<IVRInteractableObj>();
                if (iobjs.Length == 0)
                {
                    iobjs = currentInteractableObj.GetComponentsInParent<IVRInteractableObj>();
                }
                for (int i = 0; i < iobjs.Length; i++)
                {
                    iobjs[i].OnRayOut(new VRRayCastArgs(hit, trackedObj));
                }
                currentInteractableObj = null;
            }
        }
        endPosition = lineEndPos;

        if (line)
        {
            line.SetLineEndPos(endPosition);
        }
        currentHit = hit;
        return cast;
    }
}
