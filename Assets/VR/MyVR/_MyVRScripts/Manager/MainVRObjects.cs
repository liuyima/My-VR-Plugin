using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainVRObjects : MonoBehaviour {

    private void Awake()
    {
        gameObject.hideFlags = HideFlags.DontSave;
        DontDestroyOnLoad(gameObject);
    }
}
