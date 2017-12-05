using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleRingButton : MenuButton {
    bool isIn = false;
    Image image;
    Vector3 originalLocalPos;
    Quaternion originalLocalRot;
    protected override void Awake()
    {
        base.Awake();
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

    public override void RestoreButton()
    {
        transform.localPosition = originalLocalPos;
        transform.localRotation = originalLocalRot;
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
