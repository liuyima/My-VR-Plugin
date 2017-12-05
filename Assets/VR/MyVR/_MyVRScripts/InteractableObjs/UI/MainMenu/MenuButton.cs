using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [HideInInspector]
    public VREvent myEvent;
    [HideInInspector]
    public Text text;

    protected virtual void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SetButton(Operations operation)
    {
        text.text = operation.name;
        myEvent = operation.events;
    }
    public void InvokeMyEvent()
    {
        myEvent.Invoke();
        Manager.manager.audioManager.PlaySelectAudio();
    }
    public virtual void RestoreButton()
    {
        text.text = "";
        myEvent = null;
    }
}
