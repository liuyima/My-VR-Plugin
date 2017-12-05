using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public struct Operations
{
    public string name;
    public VREvent events;
    public void SetOptions(VREvent events,string name)
    {
        this.name = name;
        this.events = events;
    }

    public Operations(string name,VREvent vrEvent)
    {
        this.name = name;
        this.events = vrEvent; 
    }

    public Operations(string name, UnityAction function)
    {
        this.name = name;
        this.events = new VREvent();
        events.AddListener(function);
    }
}
