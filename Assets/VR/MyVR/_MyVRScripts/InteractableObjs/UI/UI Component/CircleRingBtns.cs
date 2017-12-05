using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleRingBtns : MonoBehaviour {

    public Transform center;
    public Transform needle;
    public CircleRingButton[] btns;
    public iTween.EaseType easeType;
    public delegate void OnHideOption();
    event OnHideOption onHideOptionEvent;
    public Text nameText;
    bool isOpen = false;
    CircleRingButton[] currentShowBtns;
    int currentTouchBtnIndex = -1;
    float startAng;
    Vector3 originalLocalPos;
    Quaternion originalLocalRot;
	// Use this for initialization
	void Start () {
        Manager.manager.trackedObjectManager.getPadTouch += TouchBtn;
        Manager.manager.trackedObjectManager.getPadTouchUp += TouchUp;
        Manager.manager.trackedObjectManager.getPadPressDown += OnPressDon;
        originalLocalPos = transform.localPosition;
        originalLocalRot = transform.localRotation;
    }


    public void TouchBtn(SteamVR_TrackedObject trackedObj,Vector2 axis)
    {
        if (trackedObj.index == Manager.manager.trackedObjectManager.left.index && isOpen && currentShowBtns!=null)
        {
            needle.gameObject.SetActive(true);
            Vector3 le = needle.localEulerAngles;
            float touchAng = axis.x < 0 ? Vector2.Angle(new Vector2(0, 1), axis.normalized) : -Vector2.Angle(new Vector2(0, 1), axis.normalized);
            le.z = -(touchAng - 90f);
            needle.localEulerAngles = le;
            int index = (int)(touchAng - startAng) / 30;
            index = Mathf.Clamp(index, 0, currentShowBtns.Length - 1);
            if (index != currentTouchBtnIndex)
            {
                if (currentTouchBtnIndex >= 0 && currentTouchBtnIndex < currentShowBtns.Length)
                {
                    currentShowBtns[currentTouchBtnIndex].OnTouchOut();
                }
                currentShowBtns[index].OnTouchIn();
                currentTouchBtnIndex = index;
            }
        }
    }

    void TouchUp(SteamVR_TrackedObject trackedObj, Vector2 axis)
    {
        if (trackedObj.index == Manager.manager.trackedObjectManager.left.index && currentTouchBtnIndex >= 0 && currentTouchBtnIndex < currentShowBtns.Length)
        {
            needle.gameObject.SetActive(false);
            currentShowBtns[currentTouchBtnIndex].OnTouchOut();
            currentTouchBtnIndex = -1;
        }
    }

    void OnPressDon(SteamVR_TrackedObject trackedObj, Vector2 axis)
    {
        if (trackedObj.index == Manager.manager.trackedObjectManager.left.index && currentTouchBtnIndex >= 0 && currentTouchBtnIndex < currentShowBtns.Length)
        {
            currentShowBtns[currentTouchBtnIndex].InvokeMyEvent();
        }
    }

    public void SetBtns(Operations[] operations,string optionName, OnHideOption onHideOption)
    {
        RestoreBtns();
        onHideOptionEvent += onHideOption;
        currentShowBtns = new CircleRingButton[operations.Length];
        for (int i = 0; i < currentShowBtns.Length; i++)
        {
            currentShowBtns[i] = btns[i];
            currentShowBtns[i].gameObject.SetActive(true);
            currentShowBtns[i].SetButton(operations[i]);
        }
        SetBtnsPosition(currentShowBtns);
        SetCenterRotationByBtnNum(currentShowBtns.Length);
        nameText.text = optionName;
    }

    public void ChangeOneBtn(string originalBtnName,Operations newOp)
    {
        foreach (CircleRingButton btn in currentShowBtns)
        {
            if (btn.text.text == originalBtnName)
            {
                btn.SetButton(newOp);
                return;
            }
        }
    }

    public void HideMenu()
    {
        RestoreBtns();
        if (currentShowBtns != null)
        {
            ShowHide(false, currentShowBtns.Length);
        }
    }

    public void ShowMenu()
    {
        ShowHide(true, currentShowBtns.Length);
        Manager.manager.trackedObjectManager.ShockHand(Manager.manager.trackedObjectManager.left, 0.25f);
    }

    void SetCenterRotationByBtnNum(int btnNum)
    {
        float ang = 90 + btnNum * 15;
        center.localEulerAngles = new Vector3(0, 0, ang);
    }

    void ShowHide(bool b,int btnNum)
    {
        isOpen = b;
        nameText.gameObject.SetActive(b);
        float targetAng = b ? 0 : 90 + btnNum * 15;
        iTween.RotateTo(center.gameObject, iTween.Hash("z", targetAng,"islocal",true, "time", 0.55f, "easetype", easeType));
    }

    void SetBtnsPosition(CircleRingButton[] btns)
    {
        int mid;
        if (btns.Length % 2 == 1)
        {
            mid = btns.Length / 2;
            for (int i = 0; i < btns.Length; i++)
            {
                float ang = (i - mid) * 30;
                btns[i].transform.RotateAround(center.position, center.forward, ang);
                if (i == 0)
                {
                    startAng = ang - 15;
                }
            }
        }
        else
        {
            mid = btns.Length / 2;
            for (int i = 0; i < btns.Length; i++)
            {
                float ang = (i - mid) * 30 + 15;
                btns[i].transform.RotateAround(center.position, center.forward, ang);
                if (i == 0)
                {
                    startAng = ang - 15;
                }
            }
        }
    }

    void RestoreBtns()
    {
        Debug.Log("restore buttons");
        if (onHideOptionEvent != null)
        {
            onHideOptionEvent.Invoke();
            onHideOptionEvent = null;
        }
        isOpen = false;
        if (currentTouchBtnIndex != -1)
        {
            currentShowBtns[currentTouchBtnIndex].OnTouchOut();
            currentTouchBtnIndex = -1;
        }
        if (currentShowBtns != null)
        {
            for (int i = 0; i < currentShowBtns.Length; i++)
            {
                currentShowBtns[i].gameObject.SetActive(false);
                currentShowBtns[i].RestoreButton();
            }
        }
        currentShowBtns = null;
        nameText.text = "";
        nameText.gameObject.SetActive(false);
        needle.gameObject.SetActive(false);
        center.localEulerAngles = new Vector3(0, 0, 105);
    }
}
