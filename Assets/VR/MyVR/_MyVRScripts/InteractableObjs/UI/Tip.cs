using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour {
    public Text text;
    EmitRay ray;
    GameObject currentShow = null;

    // Use this for initialization
    void Start () {
        ray = Manager.manager.trackedObjectManager.right.GetComponent<EmitRay>();
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        SetPos();
	}

    public void ShowSign(string information,GameObject show)
    {
        currentShow = show;
        text.text = information;
        gameObject.SetActive(true);
    }

    public void HideSign(GameObject hide)
    {
        if (currentShow == hide)
        {
            currentShow = null;
            gameObject.SetActive(false);
        }
    }

    void SetPos()
    {
        transform.position = ray.endPosition;
        transform.forward = (transform.position - Manager.manager.trackedObjectManager.eye.position).normalized;
    }
}
