using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainMenu : MonoBehaviour {

    public delegate void OnHideOption();
    protected event OnHideOption onHideOptionEvent;

    public abstract void SetBtns(Operations[] operations, string optionName, OnHideOption onHideOption);
    public abstract void ChangeOneBtn(string originalBtnName, Operations newOp);
    public abstract void HideMenu();
    public abstract void ShowMenu();
    protected virtual void RestoreBtns()
    {
        Debug.Log("restore buttons");
        if (onHideOptionEvent != null)
        {
            onHideOptionEvent.Invoke();
            onHideOptionEvent = null;
        }
    }
}
