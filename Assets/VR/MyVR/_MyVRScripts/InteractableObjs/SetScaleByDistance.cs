using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScaleByDistance : MonoBehaviour {
    Transform mainCamera;
    float originalDis;
    // Use this for initialization
    void Start () {
        mainCamera = Manager.manager.trackedObjectManager.eye;
        originalDis = 3;
    }
	
	// Update is called once per frame
	void Update () {
        SetScale();
	}

    public void SetScale()
    {
        float ch = Vector3.Distance(transform.position, mainCamera.transform.position);
        float s = ch / originalDis;
        originalDis = ch;
        transform.localScale *= s;
    }
}
