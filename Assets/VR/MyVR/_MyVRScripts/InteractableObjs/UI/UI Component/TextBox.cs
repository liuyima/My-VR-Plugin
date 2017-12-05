using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {
    public GameObject textPref;
    ContentAdaptation contentAdaptation;

    private void Awake()
    {
        contentAdaptation = GetComponent<ContentAdaptation>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void AddNewDialogText(string dt)
    {
        GameObject newText = Instantiate(textPref);
        newText.transform.SetParent(transform);
        newText.transform.localScale = Vector3.one;
        newText.transform.localPosition = Vector3.zero;
        newText.GetComponent<Text>().text = dt;
        newText.GetComponent<TextAdaptation>().ResetSize();
        contentAdaptation.AddOneContent(newText.GetComponent<RectTransform>(), true);
    }

    public void AddOneContent(RectTransform rt,bool setX0)
    {
        contentAdaptation.AddOneContent(rt, setX0);
    }

}
