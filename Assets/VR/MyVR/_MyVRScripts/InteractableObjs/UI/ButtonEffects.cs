using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ButtonEffects{// : VREffects {

    public enum Transition { Color, Sprite }
    public Transition transition;

    public Image image;
    public Color normalColor = Color.white, hoverColor = Color.white, disableColor = Color.gray;
    public Sprite normalSprite, hoverSprite, disableSprite;

    protected void ChangeColor(Color c)
    {
        image.color = c;
    }

    protected void Scale(bool add)
    {

    }

    public void ShowHide(bool b)
    {
        
    }

    void ChangeSprite(Sprite sprite)
    {

    }

    public void SetNoraml()
    {
        if (transition == Transition.Color)
        {
            ChangeColor(normalColor);
        }
        else
        {
            ChangeSprite(normalSprite);
        }
    }
    public void SetHover()
    {
        if (transition == Transition.Color)
        {
            ChangeColor(hoverColor);
        }
        else
        {
            ChangeSprite(hoverSprite);
        }
    }
    public void SetDisable()
    {
        if (transition == Transition.Color)
        {
            ChangeColor(disableColor);
        }
        else
        {
            ChangeSprite(disableSprite);
        }
    }

}
