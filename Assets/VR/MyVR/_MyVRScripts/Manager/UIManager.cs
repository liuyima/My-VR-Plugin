using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour {
    public CircleRingBtns circleRingBtns;
    public Tip informationSign;
    Operations[] mainMenuOperations;
    bool mainMenuIsShowing = false;

    // Use this for initialization
    void Start() {
        Manager.manager.trackedObjectManager.getMenuDown += ShowHideMainMenu;
    }

    /// <summary>
    /// 显示、隐藏主菜单
    /// </summary>
    /// <param name="trackedObj"></param>
    void ShowHideMainMenu(SteamVR_TrackedObject trackedObj)
    {
        if (mainMenuIsShowing)
        {
            circleRingBtns.HideMenu();
            mainMenuIsShowing = false;
        }
        else
        {
            Manager.manager.audioManager.PlayMainMenuAudio();
            mainMenuIsShowing = true;
            circleRingBtns.SetBtns(mainMenuOperations, "主菜单", null);
            circleRingBtns.ShowMenu();
        }
    }

    /// <summary>
    /// 设置场景中的主要菜单选项
    /// </summary>
    /// <param name="operations"></param>
    public void SetMainMenuOperations(Operations[] operations)
    {
        mainMenuOperations = operations;
        if (mainMenuIsShowing)
        {
            mainMenuIsShowing = false;
            circleRingBtns.SetBtns(mainMenuOperations, "主菜单", null);
        }
    }

    public void ShowOperations(Operations[] operations,string name, CircleRingBtns.OnHideOption onHideOption)
    {
        if (mainMenuIsShowing)
        {
            circleRingBtns.HideMenu();
            mainMenuIsShowing = false;
        }
        circleRingBtns.SetBtns(operations, name, onHideOption);
        circleRingBtns.ShowMenu();
    }

    /// <summary>
    /// 显示场景中的UI
    /// </summary>
    /// <param name="position"></param>
    /// <param name="ui"></param>
    public static void ShowUI(Vector3 position,Transform ui)
    {
        ui.gameObject.SetActive(true);
        Bounds b = OperationOfModel.GetAABB(ui);
        position.y += b.extents.y;
        ui.position = position;
        Vector3 forw = (position - Manager.manager.trackedObjectManager.eye.position).normalized;
        forw.y = 0;
        ui.transform.forward = forw;
    }

}
