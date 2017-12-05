using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *功能：text自适应 
 */

public class TextAdaptation : MonoBehaviour {
    RectTransform rectTrans;
    Text text;
    Vector2 origPos;
    float prevHeight;
    ContentSizeFitter csf;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

    void Start () {
        origPos = rectTrans.localPosition;
        prevHeight = rectTrans.rect.height;
    }



    void Update()
    {
    }  

    public void ResetSize()
    {
        Vector2 v = rectTrans.sizeDelta;
        v.y = text.preferredHeight;
        rectTrans.sizeDelta = v;
        Vector2 ap = rectTrans.anchoredPosition;
        ap.y = 0 - v.y*0.5f;
        rectTrans.anchoredPosition = ap;

        Vector2 s = transform.parent.GetComponent<RectTransform>().sizeDelta;
        s.y = text.preferredHeight;
        transform.parent.GetComponent<RectTransform>().sizeDelta = s;
    }
                             
}
