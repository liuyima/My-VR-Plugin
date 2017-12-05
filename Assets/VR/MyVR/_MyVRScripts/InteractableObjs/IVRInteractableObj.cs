using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *功能：VR下所有可交互的物体继承此接口。 
 */

public interface IVRInteractableObj
{
    /// <summary>
    /// 当设备发射的射线检测到
    /// </summary>
    void OnRayIn(VRRayCastArgs rayArgs);
    /// <summary>
    /// 当设备发射的射线退出
    /// </summary>
    void OnRayOut(VRRayCastArgs rayArgs);
    /// <summary>
    /// 当设备发射的射线停留
    /// </summary>
    void OnRayStay(VRRayCastArgs rayArgs);
}
