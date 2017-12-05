using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class VREvent : UnityEvent {
    public VREvent()
    { }
    public VREvent(UnityAction function)
    {
        AddListener(function);
    }
}
