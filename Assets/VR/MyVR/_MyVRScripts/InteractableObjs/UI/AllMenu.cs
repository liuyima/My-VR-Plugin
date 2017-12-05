using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMenu : MonoBehaviour {

    public bool resetPosition = true;
    public bool isShowing { get; private set; }
    Transform eye;
    float originalY;
    float dis = 1.5f;
    // Use this for initialization
    void Start () {
        eye = Manager.manager.trackedObjectManager.eye;
        Vector3 pf = eye.forward;
        Vector3 mf = (transform.position - eye.position).normalized;
        pf.y = 0;
        mf.y = 0;
        originalY = transform.position.y;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (resetPosition)
        {
            Vector3 forward = (eye.position - transform.position).normalized;
            forward.y = 0;
            transform.forward = -forward;
        }
    }

    public void ShowMenu()
    {
        if (resetPosition)
        {
            ResetPosition();
        }
        isShowing = true;
        gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        isShowing = false;
        gameObject.SetActive(false);
    }

    void ResetPosition()
    {
        Vector3 f = eye.forward;
        f.y = 0;
        Vector3 dir = f;
        Vector3 np = eye.position + dir.normalized * dis;
        np.y = originalY;
        transform.position = np;
    }

}
