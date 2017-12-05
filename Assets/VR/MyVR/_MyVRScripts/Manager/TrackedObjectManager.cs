using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/*
 *功能：管理两个手柄的输入 
 */

public class TrackedObjectManager : MonoBehaviour {

    public SteamVR_TrackedObject left;
    public SteamVR_TrackedObject right;
    public Transform eye;

    public delegate void GetInput(SteamVR_TrackedObject trackedObj);
    public event GetInput getTriggerPressDown;
    public event GetInput getTriggerPressUp;
    public event GetInput getTriggerPress;

    public event GetInput getGripPressDown;
    public event GetInput getGripPress;
    public event GetInput getGripPressUp;

    public event GetInput getMenuDown;

    public delegate void GetPadInput(SteamVR_TrackedObject trackedObj, Vector2 axis);
    public event GetPadInput getPadTouch;
    public event GetPadInput getPadPressDown;
    public event GetPadInput getPadPress;
    public event GetPadInput getPadPressUp;
    public event GetPadInput getPadTouchDown;
    public event GetPadInput getPadTouchUp;

    bool shockHand = false;
    
    // Update is called once per frame
    void Update()
    {
        GetTrackedInput();
    }

    void GetTrackedInput()
    {
        var ldevice = SteamVR_Controller.Input((int)left.index);
        var rdevice = SteamVR_Controller.Input((int)right.index);
        GetOneControllerInput(ldevice, left);
        GetOneControllerInput(rdevice, right);
    }

    /// <summary>
    /// 接收一个手柄输入
    /// </summary>
    /// <param name="device">手柄</param>
    /// <param name="deviceIndex">手柄的序号</param>
    void GetOneControllerInput(SteamVR_Controller.Device device, SteamVR_TrackedObject trackedObj)
    {
        //=============================手柄的触发键==================================================================
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && getTriggerPressDown != null)
        {
            getTriggerPressDown(trackedObj);
        }
        else if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && getTriggerPress != null)
        {
            getTriggerPress(trackedObj);
        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && getTriggerPressUp != null)
        {
            getTriggerPressUp(trackedObj);
        }
        //=============================手柄两侧的grip键==============================================================
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && getGripPressDown != null)
        {
            getGripPressDown(trackedObj);
        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip) && getGripPressUp != null)
        {
            getGripPressUp(trackedObj);
        }
        else if (device.GetPress(SteamVR_Controller.ButtonMask.Grip) && getGripPress != null)
        {
            getGripPress(trackedObj);
        }
        //==============================手柄圆盘=====================================================================
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) && getPadTouchDown != null)
        {
            Vector2 lpadAxis = device.GetAxis();
            getPadTouchDown(trackedObj, lpadAxis);
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && getPadTouch != null)
        {
            Vector2 lpadAxis = device.GetAxis();
            getPadTouch(trackedObj, lpadAxis);
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && getPadPressDown != null)
        {
            Vector2 lpadAxis = device.GetAxis();
            getPadPressDown(trackedObj, lpadAxis);
        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad) && getPadTouchUp != null)
        {
            Vector2 lpadAxis = device.GetAxis();
            getPadTouchUp(trackedObj, lpadAxis);
        }
        
        //===============================手柄菜单键==================================================================
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && getMenuDown != null)
        {
            getMenuDown(trackedObj);
        }
        else if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && getPadPress != null)
        {
            Vector2 lpadAxis = device.GetAxis();
            getPadPress(trackedObj, lpadAxis);
        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && getPadPressUp != null)
        {
            Vector2 lpadAxis = device.GetAxis();
            getPadPressUp(trackedObj, lpadAxis);
        }
    }

    public void EnableGrab(bool b)
    {
        left.transform.GetComponentInChildren<VRTK_ControllerEvents>().enabled = b;
        right.transform.GetComponentInChildren<VRTK_ControllerEvents>().enabled = b;
    }

    public void ShockHand(SteamVR_TrackedObject trackedObj,float durationTime)
    {
        int toindex = (int)trackedObj.index;
        StartCoroutine(ShockHand(toindex, durationTime));
    }

    IEnumerator ShockHand(int toIndex, float durationTime)
    {
        shockHand = true;
        Invoke("StopShock", durationTime);
        var device = SteamVR_Controller.Input(toIndex);
        while (shockHand)
        {
            device.TriggerHapticPulse(500);
            yield return new WaitForEndOfFrame();
        }
    }

    void StopShock()
    {
        shockHand = false;
    }


}
