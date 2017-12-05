using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAdaptation : MonoBehaviour {
    public float topOffset;
    public float space;
    List<RectTransform> rectTrans = new List<RectTransform>();
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddOneContent(RectTransform newRectTran,bool setX0)
    {
        newRectTran.anchorMax = new Vector2(0.5f, 1);
        newRectTran.anchorMin = new Vector2(0.5f, 1);
        newRectTran.anchoredPosition = GetPositionByPrevContent(newRectTran,setX0);
        rectTrans.Add(newRectTran);
        SetPanelHeight(rectTrans[rectTrans.Count - 1].sizeDelta.y + space);
    }

    Vector2 GetPositionByPrevContent(RectTransform newRectTran, bool setX0)
    {
        Vector2 newPos;
        if (rectTrans.Count > 0)
        {
            RectTransform prevRT = rectTrans[rectTrans.Count - 1];
            newPos = setX0 ? Vector2.zero : prevRT.anchoredPosition;
            newPos.y = prevRT.anchoredPosition.y - prevRT.sizeDelta.y * 0.5f - newRectTran.sizeDelta.y * 0.5f;
            newPos.y -= space;
        }
        else
        {
            newPos = newRectTran.anchoredPosition;
            newPos.y =  -newRectTran.sizeDelta.y * 0.5f;
            newPos.y -= topOffset;
        }
        return newPos;
    }

    void SetPanelHeight(float addHeight)
    {
        Vector2 sd = rectTransform.sizeDelta;
        sd.y += addHeight;
        rectTransform.sizeDelta = sd;
    }
}
