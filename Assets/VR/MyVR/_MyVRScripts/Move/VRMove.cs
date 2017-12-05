using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMove : MonoBehaviour {

    public SteamVR_TrackedObject useTrackeObj;
    public MoveTargetPointEffects targetPointEffects;
    public bool movable { get { return _movable; }set { _movable = value; } }
    bool _movable = true;
    public string currentMovableTag = TagManager.Terrain;
    TrackedObjectManager trackedObjManager;
    Vector3 point;
    bool hittingTerrain = false;
    bool readyToMove = false;

	// Use this for initialization
	void Awake () {
        trackedObjManager = Manager.manager.trackedObjectManager;
        trackedObjManager.getPadTouchUp += CancelMove;
        trackedObjManager.getPadTouchDown += ReadyToMove;
        trackedObjManager.getPadTouch += ReadyToMove;
        trackedObjManager.getPadPressDown += Move;
        useTrackeObj.GetComponent<EmitRay>().onHitCollider += HitTerrain;
        
	}

    /// <summary>
    /// 改变坐标
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="fade">是否启用淡入淡出</param>
    public void ChangePlayerPos(Vector3 pos, bool fade = false)
    {
        if (fade)
        {
            SteamVR_Fade.Start(Color.black, 0.2f);
            StartCoroutine(Move(pos, 0.2f));
        }
        else
        {
            Vector3 offset = Manager.manager.trackedObjectManager.eye.position - transform.position;
            offset.y = 0;
            transform.position = pos - offset;
        }
    }

    public void ChangeCurrentMovableTag(bool isTerrain)
    {
        currentMovableTag = isTerrain ? TagManager.Terrain : TagManager.TowerDeck;
    }

    /// <summary>
    /// 改变朝向
    /// </summary>
    /// <param name="direct"></param>
    public void ChangeHeadDir(Vector3 direct)
    {
        direct.y = 0;
        Vector3 headF = Manager.manager.trackedObjectManager.eye.forward;
        headF.y = 0;
        Vector3 headR = Manager.manager.trackedObjectManager.eye.transform.right;
        headR.y = 0;
        float dot = Vector3.Dot(headR.normalized, direct.normalized);
        float onRight = dot / Mathf.Abs(dot);
        float ang = Vector3.Angle(headF, direct);
        Debug.Log(ang);
        transform.Rotate(Vector3.up * onRight, ang);
    }

    IEnumerator Move(Vector3 pos,float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 offset = Manager.manager.trackedObjectManager.eye.position - transform.position;
        offset.y = 0;
        transform.position = pos - offset;
        SteamVR_Fade.Start(Color.clear, 0.2f);
    }

    void ReadyToMove(SteamVR_TrackedObject trackedObj, Vector2 axis)
    {
        if (!readyToMove && movable && hittingTerrain && trackedObj.index == useTrackeObj.index)
        {
            targetPointEffects.ReadyToMove();
            readyToMove = true;
        }
    }

    void CancelMove(SteamVR_TrackedObject trackedObj, Vector2 axis)
    {
        if (movable && trackedObj.index == useTrackeObj.index && hittingTerrain)
        {
            targetPointEffects.CancelMove();
            readyToMove = false;
        }
    }

    void Move(SteamVR_TrackedObject trackedObj, Vector2 axis)
    {
        if (movable && trackedObj.index == useTrackeObj.index && hittingTerrain)
        {
            ChangePlayerPos(point);
        }
    }

    void HitTerrain(bool hitCollider, RaycastHit hit)
    {
        if (movable)
        {
            point = hit.point;
            hittingTerrain = (hitCollider && hit.collider.tag == TagManager.Terrain);
            targetPointEffects.SetPosition(point);
            targetPointEffects.SetEffectsActive(hittingTerrain);
        }
    }
}
