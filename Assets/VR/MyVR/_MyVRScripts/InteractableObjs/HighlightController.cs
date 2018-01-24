using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;
using System;

public class HighlightController : MonoBehaviour, IVRInteractableObj
{

    Highlighter myHighlighter;
    List<Highlighter> highlighters = new List<Highlighter>();
    //public bool left,right;
    public Color highlightColor;
    [HideInInspector]
    public bool on = false;
    public bool twinkle = false;
    public bool enableHighlight = true;
    bool isOn = false;

    // Use this for initialization
    void Start()
    {
        if (!myHighlighter)
        {
            myHighlighter = GetComponent<Highlighter>();
        }
    }

    public void LightSwitch(bool b)
    {
        if (b && !isOn)
        {
            isOn = true;
            HighlightOn();
        }
        else if (!on && isOn)
        {
            isOn = false;
            HighlightOff();
        }
    }

    public void SetEnable(bool b)
    {
        enableHighlight = b;
        if (!b)
        {
            LightSwitch(false);
        }
    }

    void HighlightOn()
    {
        if (twinkle)
        {
            myHighlighter.FlashingOn(1);
            foreach (Highlighter h in highlighters)
            {
                h.FlashingOn(1);
            }
        }
        else
        {
            myHighlighter.ConstantOn(highlightColor);
            foreach (Highlighter h in highlighters)
            {
                h.ConstantOn(highlightColor);
            }
        }
    }

    void HighlightOff()
    {
        if (twinkle)
        {
            myHighlighter.FlashingOff();
            foreach (Highlighter h in highlighters)
            {
                h.FlashingOff();
            }
        }
        else
        {
            myHighlighter.ConstantOff();
            foreach (Highlighter h in highlighters)
            {
                h.ConstantOff();
            }
        }
    }

    void IVRInteractableObj.OnRayIn(VRRayCastArgs rayArgs)
    {
        if (rayArgs.trackedObj.index == Manager.manager.trackedObjectManager.right.index)
        {
            if (enableHighlight)
            {
                LightSwitch(true);
            }
        }
    }

    void IVRInteractableObj.OnRayOut(VRRayCastArgs rayArgs)
    {
        if (rayArgs.trackedObj.index == Manager.manager.trackedObjectManager.right.index)
        {
            if (enableHighlight)
            {
                LightSwitch(false);
            }
        }
    }

    void IVRInteractableObj.OnRayStay(VRRayCastArgs rayArgs)
    {
    }
}
