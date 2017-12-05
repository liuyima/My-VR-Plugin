using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public GodViewPoint firstGodViewPoint;
    public Material godViewSkyBox;
    public float moveSpeed;
    //public float maxDis, minDis;
    public Vector3 maxBoundary, minBoundary;
    public delegate void OnChangeView(ViewType cuurentType);
    public event OnChangeView onChangeView;
    public enum ViewType { god, normal }
    ViewType currentView = ViewType.normal;
    Vector3 originalPos;
    float movePower = 0;
    float maxMovePower = 10;
    Material originalSky;
    // Use this for initialization
    void Start()
    {
        originalPos = transform.position;
        originalSky = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentView == ViewType.god)
        //{
        //    movePower = Mathf.Lerp(movePower, 0, Time.deltaTime);
        //    ViewMove(movePower);
        //}
    }

    public void ChangeView(ViewType viewType)
    {
        if (viewType != currentView)
        {
            if (viewType == ViewType.normal)
            {
                currentView = ViewType.normal;
                Camera.main.farClipPlane = 1000;
                Camera.main.nearClipPlane = 0.05f;
                transform.rotation = Quaternion.identity;
                transform.position = originalPos;
                Manager.manager.vrMove.movable = true;
                RenderSettings.skybox = originalSky;
            }
            else
            {
                currentView = ViewType.god;
                Camera.main.farClipPlane = 3000;
                Camera.main.nearClipPlane = 0.1f;
                originalPos = transform.position;
                firstGodViewPoint.SelectThisPoint();
                Manager.manager.vrMove.movable = false;
                RenderSettings.skybox = godViewSkyBox;
            }
            Manager.manager.uiManager.circleRingBtns.HideMenu();
            if (onChangeView != null)
            {
                onChangeView(currentView);
            }
        }
    }

    public void SetMovePower(float m)
    {
        movePower += m;
        movePower = Mathf.Clamp(movePower, -maxMovePower, maxMovePower);
    }

    public void SetMovePowerZero()
    {
        movePower = 0;
    }

    void ViewMove(float f)
    {
        Vector3 moveDis = Manager.manager.trackedObjectManager.eye.forward * f * moveSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + moveDis;
        newPos.y = Mathf.Clamp(newPos.y, minBoundary.y, maxBoundary.y);
        newPos.x = Mathf.Clamp(newPos.x, minBoundary.x, maxBoundary.x);
        newPos.z = Mathf.Clamp(newPos.z, minBoundary.z, maxBoundary.z);
        transform.position = newPos;
    }
}
