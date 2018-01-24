using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
    public bool lock_X, lock_Y, lock_Z;
    public enum ForwardAxis { _X,_X0,_Y,_Y0,_Z,_Z0}
    public ForwardAxis forwarAxis;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LookAt();
	}

    void LookAt()
    {
        Vector3 forward = Manager.manager.trackedObjectManager.eye.position - transform.position;
        if (lock_X)
        {
            forward.x = 0;
        }
        if (lock_Y)
        {
            forward.y = 0;
        }
        if (lock_Z)
        {
            forward.z = 0;
        }
        switch (forwarAxis)
        {
            case ForwardAxis._X:
                transform.right = forward;
                break;
            case ForwardAxis._X0:
                transform.right = -forward;
                break;
            case ForwardAxis._Y:
                transform.up = forward;
                break;
            case ForwardAxis._Y0:
                transform.up = -forward;
                break;
            case ForwardAxis._Z:
                transform.forward = forward;
                break;
            case ForwardAxis._Z0:
                transform.forward = -forward;
                break;
        }
    }
}
