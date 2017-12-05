using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleRingButton : MonoBehaviour {
    [HideInInspector]
    public VREvent myEvent;
    [HideInInspector]
    public Text text;
    bool isIn = false;
    Image image;
    Vector3 originalLocalPos;
    Quaternion originalLocalRot;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
        originalLocalPos = transform.localPosition;
        originalLocalRot = transform.localRotation;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetButton(Operations operation)
    {
        text.text = operation.name;
        myEvent = operation.events;
    }

    public void RestoreButton()
    {
        text.text = "";
        myEvent = null;
        transform.localPosition = originalLocalPos;
        transform.localRotation = originalLocalRot;
    }

    public void InvokeMyEvent()
    {
        myEvent.Invoke();
        Manager.manager.audioManager.PlaySelectAudio();
    }

    public void OnTouchIn()
    {
        isIn = true;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one * 1.25f, "time", 0.35f));
        image.color = Color.cyan;
        Manager.manager.audioManager.PlayButtonAudio();
    }
    public void OnTouchOut()
    {
        isIn = false;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one, "time", 0.35f));
        image.color = Color.white;
    }
}
