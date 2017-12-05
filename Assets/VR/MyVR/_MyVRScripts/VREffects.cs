using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class VREffects : MonoBehaviour{

    public abstract void Scale(bool add);
    public abstract void ChangeColor(Color c);
    public abstract void ShowHide(bool b);
}
