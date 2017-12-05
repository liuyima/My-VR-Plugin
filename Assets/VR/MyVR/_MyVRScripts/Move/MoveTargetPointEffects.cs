using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetPointEffects : MonoBehaviour {

    public MeshRenderer meshRenderer;
    public float maxValue;

    public void SetPosition(Vector3 p)
    {
        transform.position = p;
    }

    public void SetEffectsActive(bool b)
    {
        gameObject.SetActive(b);
    }

    public void ReadyToMove()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", maxValue, "time", 0.3f, "onupdate", "ChangeAlpha"));
    }

    public void ChangeColor(Color color)
    {
        meshRenderer.material.SetColor("_Color", color);
    }

    public void CancelMove()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", maxValue, "to", 0, "time", 0.3f, "onupdate", "ChangeAlpha"));
    }

    void ChangeAlpha(float a)
    {
        meshRenderer.material.SetFloat("_Progress", a);
    }
}
