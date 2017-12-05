using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public LineRenderer line;
    public Transform endPoint;
    public Color menuColor,terrainColor,interactableObjColor,normalColor;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void SetLineEndPos(Vector3 endPos)
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPos);
        endPoint.position = endPos;
    }

    public void ChangeLineColor()
    {
        Color c = Color.red;
        line.startColor = c;
        line.endColor = c;
    }

    public void ShowHideLine(bool b)
    {
        line.enabled = b;
    }

}
