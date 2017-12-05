using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazePoint : MonoBehaviour
{

    public Image progress;
    public Image point;
    SetScaleByDistance setScale;
    // Use this for initialization
    void Start()
    {
        setScale = GetComponent<SetScaleByDistance>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetProgress(float p)
    {
        progress.fillAmount = p;
    }
    public void SetGazePointActive(bool b)
    {
        point.gameObject.SetActive(b);
    }
    public void SetPosition(Vector3 point)
    {
        transform.position = point;
        transform.forward = (point - Manager.manager.trackedObjectManager.eye.position).normalized;
        setScale.SetScale();
    }
}
